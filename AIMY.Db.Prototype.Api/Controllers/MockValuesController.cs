using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bogus;
using AIMY.Db.Prototype.Infrastructure.Context;
using AIMY.Db.Prototype.Infrastructure.Entities;
using AIMY.Db.Prototype.Api.Services;
using AIMY.Db.Prototype.Api.Configuration;

namespace AIMY.Db.Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockValuesController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IDbContextFactory<MyDbContext> _contextFactory;
        private readonly IAwsKmsService _kmsService;
        private readonly IConfiguration _configuration;

        public MockValuesController(MyDbContext context, IDbContextFactory<MyDbContext> contextFactory, IAwsKmsService kmsService, IConfiguration configuration)
        {
            _context = context;
            _contextFactory = contextFactory;
            _kmsService = kmsService;
            _configuration = configuration;
        }

        [HttpPost("FixSentimentAnalysisScores")]
        public async Task<IActionResult> FixSentimentAnalysisScores()
        {
            var interactions = await _context.UserInteractions
                .Where(ui => ui.Tool.ProductTools.Any(pt => pt.Product.Client.Organization.Key == "TEST_ORG"))
                .Where(ui => ui.InteractionAnalysisRuleResults.Any(rr => rr.AnalysisRule.Type == "mistake_analysis" && rr.Type == "sentiment_analysis" && (rr.QaScore > 1 || rr.Score > 1)))
                .Include(ui => ui.InteractionAnalysisRuleResults)
                .ThenInclude(rr => rr.AnalysisRule)
                .ToListAsync();

            foreach (var interaction in interactions)
            {
                interaction.Status = "Analyzed";

                foreach (var result in interaction.InteractionAnalysisRuleResults)
                {
                    result.Type = result.AnalysisRule.Type;

                    if (result.Type == "sentiment_analysis" && (result.QaScore > 1 || result.Score > 1))
                    {

                        if (result.QaScore > 1)
                            result.QaScore = 1;

                        if (result.Score > 1)
                            result.Score = 1;
                    }

                }
            }

            await _context.SaveChangesAsync();

            return Ok("Sentiment analysis scores have been fixed.");
        }

        [HttpPost("SeedTeamSupport")]
        public async Task<IActionResult> SeedTeamSupport()
        {
            var tool = await _context.Tools.FirstOrDefaultAsync(t => t.Id == 1);

            tool.Url = "https://app.na3.teamsupport.com/";
            tool.ApiKey = "1865425";
            tool.ApiSecret = await _kmsService.EncryptAsync("6e55ddb6-dd7c-4698-acd7-b4ef4c58cd91");
            tool.CreatedBy = "mo has something";
            tool.UpdatedBy = "mo has something";
            tool.Name = "TeamSupport";

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == 1);

            product.Name = "teamsupport";
            product.CreatedBy = "mo has something";
            product.UpdatedBy = "mo has something";
            //product.Tools.Add(tool);

            await _context.SaveChangesAsync();

            return Ok(product);
        }


        [HttpPost("SeedZendeskWithClient")]
        public async Task<IActionResult> SeedZendesk()
        {
            var allTool = await _context.Tools.ToListAsync();

            var tool = new Tool();

            tool.Url = "https://upland.zendesk.com/";
            tool.ApiKey = "jira_integration@mobilecommons.com/token";
            tool.ApiSecret = await _kmsService.EncryptAsync("q3n8cCANrXlDCIg8fpYX6WSsBmagkqiOV3mGCBh3");
            tool.CreatedBy = "Mohamed Hassan Ahmed Abdella";
            tool.UpdatedBy = "Mohamed Hassan Ahmed Abdella";
            tool.Name = "Zendesk";
            tool.AccessType = "API";

            int[] productsIdsToSearch = [50, 51, 52, 53];

            var productsToAdd = await _context.Products
                .Include(p => p.ProductTools)
                .Where(p => productsIdsToSearch.Contains(p.Id))
                .ToListAsync();

            var toolAdded = await _context.Tools.AddAsync(tool);
            var result = await _context.SaveChangesAsync();

            foreach (var product in productsToAdd)
            {
                if (product.ProductTools.Any(pt => pt.ToolId == tool.Id))
                {
                    continue; // Skip if the tool is already associated with the product
                }
                tool.ProductTools.Add(new ProductTool
                {
                    Product = product,
                    CreatedBy = "Mohamed Hassan Ahmed Abdella",
                    UpdatedBy = "Mohamed Hassan Ahmed Abdella"
                });
            }

            var result2 = await _context.SaveChangesAsync();

            return Ok($"{result} tool added successfully");
        }


        [HttpPost("RandomizeUserInteractionData")]
        public async Task<IActionResult> RandomizeUserInteractionDataAsync()
        {
            try
            {
                var userInteractionIds = await _context.UserInteractions
                    .Where(ui => ui.InteractionType == "Ticket")
                    .Select(ui => ui.Id)
                    .Distinct()
                    .ToListAsync();

                var analysisRules = await _context.AnalysisRules
                    .ToListAsync();

                int batchSize = 100;
                var batches = new List<List<int>>();

                for (int i = 0; i < userInteractionIds.Count; i += batchSize)
                {
                    batches.Add(userInteractionIds.Skip(i).Take(batchSize).ToList());
                }

                var now = DateTime.UtcNow;

                var batchTasks = batches.Select(async batchIds =>
                {
                    using var batchContext = _contextFactory.CreateDbContext();

                    try
                    {
                        using var transaction = await batchContext.Database.BeginTransactionAsync();

                        var interactions = await batchContext.UserInteractions
                            .Include(ui => ui.InteractionAnalysisRuleResults)
                            .Where(ui => batchIds.Contains(ui.Id))
                            .ToListAsync();


                        //TicketActionSeeder.AddRandomTicketActionsToInteractions(interactions);

                        foreach (var interaction in interactions)
                        {
                            TicketRuleResultSeeder.AddRandomTicketRuleResultsToInteraction(interaction, analysisRules);
                            //TicketHistorySeeder.AddRandomTicketHistoriesToInteraction(interaction, 3, 6);
                            //MinuplateTicketHistory(interaction);
                            //MinuplateDates(now, interaction);
                            //MinuplateExternalId(interaction);
                            //MinuplateTicketNumber(interaction);
                        }

                        await batchContext.SaveChangesAsync();

                        await transaction.CommitAsync();
                        Console.WriteLine($"Batch of {interactions.Count} interactions processed successfully.");
                        return (success: true, message: $"Successfully processed batch of {interactions.Count} interactions");
                    }
                    catch (Exception ex)
                    {
                        return (success: false, message: $"Error in batch: {ex.Message}");
                    }
                }).ToList();

                var results = await Task.WhenAll(batchTasks);

                if (results.Any(r => !r.success))
                {
                    var failedMessages = results.Where(r => !r.success).Select(r => r.message);

                    return StatusCode(StatusCodes.Status500InternalServerError, $"Some batches failed: {string.Join("; ", failedMessages)}");
                }

                return Ok($"Successfully randomized {userInteractionIds.Count} user interactions in {batches.Count} batches");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("MinuplateRuleResults")]
        public async Task<IActionResult> MinuplateRuleResults()
        {
            var ruleResultIds = await _context.InteractionAnalysisRuleResults.Select(a => new
            {
                a.UserInteractionId,
                a.AnalysisRuleId
            }).ToListAsync();


            var batchSize = 100;
            var batches = new List<List<(int, int)>>();

            for (int i = 0; i < ruleResultIds.Count; i += batchSize)
            {
                batches.Add(ruleResultIds.Skip(i).Take(batchSize).Select(a => (a.UserInteractionId, a.AnalysisRuleId)).ToList());
            }

            var results = await Task.WhenAll(
                batches.Select(async batch =>
                {
                    using var batchContext = _contextFactory.CreateDbContext();
                    try
                    {
                        using var transaction = await batchContext.Database.BeginTransactionAsync();

                        var ruleResults = await batchContext.InteractionAnalysisRuleResults
                        .Where(a => batch.Select(b => b.Item1).Contains(a.UserInteractionId) && batch.Select(b => b.Item2).Contains(a.AnalysisRuleId))
                        .ToListAsync();

                        foreach (var ruleResult in ruleResults)
                        {
                            var faker = new Faker();
                            if (ruleResult.Type == "mistake_analysis")
                            {
                                ruleResult.Score = faker.Random.Decimal();
                                ruleResult.QaScore = Random.Shared.Next(1) == 0 && ruleResult.Score != 0 ? ruleResult.Score : 0;
                                ruleResult.Comment = "Random comment";
                                ruleResult.Reason = "ss";
                            }
                            else if (ruleResult.Type == "sentiment_analysis")
                            {
                                ruleResult.Score = Random.Shared.Next(0, 100);
                                ruleResult.QaScore = Random.Shared.Next(0, 100);
                                ruleResult.Comment = "Random comment";
                                ruleResult.Reason = "Random reason";
                            }
                            else if (ruleResult.Type == "semantic_analysis")
                            {
                                ruleResult.Score = Random.Shared.Next(0, 100);
                                ruleResult.QaScore = Random.Shared.Next(0, 100);
                                ruleResult.Comment = "Random comment";
                                ruleResult.Reason = "Random reason";
                            }
                            ruleResult.Comment = Random.Shared.Next(0, 2) == 0 ? "Pass" : "Fail";
                            ruleResult.UpdatedAt = DateTime.UtcNow;
                            ruleResult.UpdatedBy = "mo has something";
                        }
                        await batchContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }).ToList());
            return Ok("Rule results have been populated.");
        }



        [HttpGet("ClientsWithNoProducts")]
        public async Task<IActionResult> GetClientsWithNoProducts()
        {
            var clientsWithProducts = await _context.Products.Select(p => new { p.Id, p.ClientId }).OrderBy(a => a.ClientId).ToListAsync();

            var clientsWithoutProducts = await _context.Clients.Where(c => !clientsWithProducts.Select(a => a.ClientId).Distinct().Contains(c.Id)).ToListAsync();

            var faker = new Faker();
            clientsWithoutProducts.Select(c =>
            {
                c.Products = new List<Product>()
                {
                    new()
                    {
                        Key = Random.Shared.Next(10000,100000).ToString(),
                        Name = faker.Company.CompanyName(),
                        CreatedBy = "mo has something",
                        UpdatedBy = "mo has something",
                    },
                    new()
                    {
                        Key = Random.Shared.Next(10000,100000).ToString(),
                        Name = faker.Company.CompanyName(),
                        CreatedBy = "mo has something",
                        UpdatedBy = "mo has something",
                    }

                };

                return c;

            }).ToList();

            await _context.SaveChangesAsync();

            return Ok(clientsWithoutProducts);
        }


        [HttpPost("SetTeamLeadersToProductWithNoTeamLeaders")]
        public async Task<IActionResult> SetTeamLeadersToProductWithNoTeamLeaders()
        {
            var products = await _context.Products.Include(p => p.ProductUsers).ToListAsync();

            var teamLeaders = await _context.Users.Where(u => u.UserRoles.Any(r => r.Role.Name == "TeamLeader")).ToListAsync();

            foreach (var product in products)
            {
                if (!product.ProductUsers.Any())
                {
                    var teamLeader = new ProductUser
                    {
                        UserId = teamLeaders[Random.Shared.Next(teamLeaders.Count)].Id,
                        ProductId = product.Id,
                        CreatedBy = "mo has something",
                        UpdatedBy = "mo has something"
                    };
                    product.ProductUsers.Add(teamLeader);
                }
            }
            await _context.SaveChangesAsync();
            return Ok("Team leaders have been set for products with no team leaders.");
        }

        [HttpGet("DecryptApiSecret/{toolId}")]
        public async Task<IActionResult> DecryptApiSecret(int toolId)
        {
            try
            {
                var tool = await _context.Tools.FirstOrDefaultAsync(t => t.Id == toolId);
                
                if (tool == null)
                {
                    return NotFound($"Tool with ID {toolId} not found");
                }

                if (tool.ApiSecret == null || tool.ApiSecret.Length == 0)
                {
                    return BadRequest("Tool has no encrypted API secret");
                }

                var decryptedSecret = await _kmsService.DecryptAsync(tool.ApiSecret);
                
                // In a real application, you wouldn't return the decrypted secret like this
                // This is just for demonstration purposes
                return Ok(new { 
                    ToolId = tool.Id,
                    ToolName = tool.Name,
                    DecryptedSecret = decryptedSecret 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Error decrypting API secret: {ex.Message}");
            }
        }

        [HttpGet("KmsConfiguration")]
        public IActionResult GetKmsConfiguration()
        {
            var kmsOptions = _configuration.GetSection("KeyManagement").Get<AwsKmsOptions>();
            
            return Ok(new
            {
                KeyId = kmsOptions?.KeyId,
                Region = kmsOptions?.Region,
                HasAccessKey = !string.IsNullOrEmpty(kmsOptions?.AccessKey),
                HasSecretKey = !string.IsNullOrEmpty(kmsOptions?.SecretKey)
            });
        }
    }

    public static class TicketActionSeeder
    {
        public static void AddRandomTicketActionsToInteractions(IEnumerable<UserInteraction> userInteractions, int minActions = 3, int maxActions = 8)
        {
            // Define common action types and actor roles
            var actionTypes = new[] { "Created", "Assigned", "StatusChange", "Comment", "AttachmentAdded", "Closed" };
            var supportTeam = new[] { "support@company.com", "john.doe@company.com", "alice.support@company.com", "tech.team@company.com" };
            var customers = new[] { "customer@example.com", "client@domain.com", "user123@gmail.com", "enterprise.client@business.org" };

            foreach (var interaction in userInteractions)
            {
                // Skip if interaction doesn't have ticket properties populated
                if (string.IsNullOrEmpty(interaction.TicketNumber) || string.IsNullOrEmpty(interaction.InteractionType) || interaction.InteractionType != "Ticket")
                    continue;

                // Generate random number of actions for this ticket
                var actionCount = new Random().Next(minActions, maxActions + 1);
                var baseDate = interaction.ExternalDateCreated ?? DateTime.UtcNow.AddDays(-new Random().Next(5, 20));
                var ticketActions = new List<TicketAction>();

                // Create ticket creation action
                ticketActions.Add(new TicketAction
                {
                    UserInteractionId = interaction.Id,
                    Type = "Created",
                    Description = interaction.TicketDescription ?? "New support ticket created",
                    TakenBy = customers[new Random().Next(customers.Length)],
                    TakenAt = baseDate
                });

                // Generate intermediate actions
                var faker = new Faker();
                for (int i = 0; i < actionCount - 2; i++) // -2 because we create first and last actions separately
                {
                    var actionType = faker.PickRandom(actionTypes.Where(t => t != "Created" && t != "Closed").ToArray());
                    var isCustomerAction = faker.Random.Bool(0.3f); // 30% chance it's from customer
                    var hoursOffset = faker.Random.Double(1, 8 * (i + 1)); // Progressive time offsets

                    var action = new TicketAction
                    {
                        UserInteractionId = interaction.Id,
                        Type = actionType,
                        Description = GetContentForAction(faker, actionType, interaction.TicketTitle),
                        TakenBy = isCustomerAction ? faker.PickRandom(customers) : faker.PickRandom(supportTeam),
                        TakenAt = baseDate.AddHours(hoursOffset)
                    };

                    ticketActions.Add(action);
                }

                // Add closing action if ticket is closed
                if (interaction.Status == "Closed" || interaction.ExternalStatus == "Closed")
                {
                    var closedDate = interaction.ExternalDateClosed ?? baseDate.AddHours(faker.Random.Double(24, 72));

                    ticketActions.Add(new TicketAction
                    {
                        UserInteractionId = interaction.Id,
                        Type = "Closed",
                        Description = $"Issue resolved: {faker.Lorem.Sentence()}",
                        TakenBy = faker.PickRandom(supportTeam),
                        TakenAt = closedDate
                    });
                }

                // Sort by date and add to interaction
                foreach (var action in ticketActions.OrderBy(a => a.CreatedAt))
                {
                    interaction.TicketActions.Add(action);
                }
            }
        }


        private static string GetContentForAction(Faker faker, string actionType, string ticketTitle)
        {
            return actionType switch
            {
                "Assigned" => faker.PickRandom(
                    "Assigned to technical support team",
                    $"Routing ticket {ticketTitle} to appropriate department",
                    "Escalated to senior support agent"),
                "StatusChange" => faker.PickRandom(
                    "Changed status to In Progress",
                    "Priority updated to High",
                    "Waiting on customer response"),
                "Comment" => faker.Lorem.Paragraph(2),
                "AttachmentAdded" => faker.PickRandom(
                    "Added screenshot of the error",
                    "Uploaded system logs for analysis",
                    "Attached troubleshooting guide"),
                "Closed" => $"Successfully resolved the issue with {ticketTitle}. {faker.Lorem.Sentence()}",
                _ => faker.Lorem.Sentence()
            };
        }
    }

    public static class TicketRuleResultSeeder
    {
        public static void AddRandomTicketRuleResultsToInteraction(UserInteraction interaction, List<AnalysisRule> analysisRules)
        {
            interaction.InteractionAnalysisRuleResults.Clear();

            Dictionary<ReviewOutcome, int> caseCounts = Enum.GetValues<ReviewOutcome>()
                .ToDictionary(outcome => outcome, _ => 0);


            foreach (AnalysisRule analysisRule in analysisRules)
            {
                int minimumCount = caseCounts.Values.Min();

                var leastUsedOutcomes = caseCounts
                    .Where(kv => kv.Value == minimumCount)
                    .Select(kv => kv.Key)
                    .ToList();

                ReviewOutcome randomCase = leastUsedOutcomes[Random.Shared.Next(leastUsedOutcomes.Count)];

                InteractionAnalysisRuleResult? ruleResult = randomCase switch
                {
                    ReviewOutcome.aiPassed => CreateRuleResult(interaction, analysisRule, analysisRule.Weight, null),
                    ReviewOutcome.aiFailed => CreateRuleResult(interaction, analysisRule, 0, null),
                    ReviewOutcome.manualPassed => CreateRuleResult(interaction, analysisRule, null, analysisRule.Weight),
                    ReviewOutcome.manualFailed => CreateRuleResult(interaction, analysisRule, null, 0),
                    ReviewOutcome.manualPassedAndAiPassed => CreateRuleResult(interaction, analysisRule, analysisRule.Weight, analysisRule.Weight),
                    ReviewOutcome.manualPassedAndAiFailed => CreateRuleResult(interaction, analysisRule, 0, analysisRule.Weight),
                    ReviewOutcome.manualFailedAndAiPassed => CreateRuleResult(interaction, analysisRule, analysisRule.Weight, 0),
                    ReviewOutcome.manualFailedAndAiFailed => CreateRuleResult(interaction, analysisRule, 0, 0),
                    ReviewOutcome.bothNotDone => CreateRuleResult(interaction, analysisRule, null, null),
                    _ => throw new ArgumentOutOfRangeException(),
                };

                if (ruleResult != null)
                {
                    interaction.InteractionAnalysisRuleResults.Add(ruleResult);
                }

                caseCounts[randomCase]++;
            }

        }

        public static InteractionAnalysisRuleResult? CreateRuleResult(UserInteraction interaction, AnalysisRule rule, decimal? score, decimal? qaScore)
        {
            if (score == null && qaScore == null)
                return null;

            var ruleResult = new InteractionAnalysisRuleResult
            {
                UserInteractionId = interaction.Id,
                AnalysisRuleId = rule.Id,
                Type = new Faker().PickRandomParam("sentiment_analysis", "mistake_analysis"),
                Score = score,
                QaScore = qaScore,
                CreatedAt = interaction.CreatedAt.AddDays(new Faker().Random.Int(1, 10)).ToUniversalTime(),
                CreatedBy = "mo has something",
                UpdatedBy = "mo has something"
            };

            ruleResult.Reason = ruleResult.Score != null ? RuleResultReasonGenerator.GetRandomReason(ruleResult.Type) : null;

            string? commentResult;

            if (ruleResult.Type == "sentiment_analysis")
            {
                commentResult = ruleResult.QaScore != null ? ruleResult.QaScore == rule.Weight ? new Faker().PickRandomParam("Positive", "Neutral") : "Negative" : null;
            }
            else
            {
                commentResult = ruleResult.QaScore != null ? ruleResult.QaScore == rule.Weight ? "Pass" : "Fail" : null;
            }

            if (commentResult != null)
            {
                ruleResult.Comment = RuleResultCommentGenerator.GetRandomComment(ruleResult.Type, commentResult);
            }

            ruleResult.QaReview = ruleResult.QaScore != null ? ruleResult.Score == ruleResult.QaScore ? new Faker().PickRandomParam("Agree", "Neutral") : new Faker().PickRandomParam("Disagree", "Neutral") : null;

            return ruleResult;
        }
    }

    public enum ReviewOutcome
    {
        aiPassed,
        aiFailed,
        manualPassed,
        manualFailed,
        manualPassedAndAiPassed,
        manualPassedAndAiFailed,
        manualFailedAndAiPassed,
        manualFailedAndAiFailed,
        bothNotDone
    }

    public static class TicketHistorySeeder
    {
        public static void AddRandomTicketHistoriesToInteraction(UserInteraction interaction, int minEntries = 2, int maxEntries = 5)
        {
            if (interaction == null || interaction.InteractionType != "Ticket")
                return;

            // Initialize ticket histories collection if null
            if (interaction.TicketHistories == null)
                interaction.TicketHistories = new List<TicketHistory>();

            // Skip if already has ticket history entries
            if (interaction.TicketHistories.Count > 0)
                interaction.TicketHistories.Clear();

            var faker = new Faker();
            var baseDate = interaction.ExternalDateCreated ?? DateTime.UtcNow.AddDays(-faker.Random.Int(1, 10)).ToUniversalTime();

            // Determine number of history entries to create
            int entriesCount = faker.Random.Int(minEntries, maxEntries);

            // Create ticket creation history
            interaction.TicketHistories.Add(new TicketHistory
            {
                UserInteractionId = interaction.Id,
                CreatedAt = baseDate,
                CreatedBy = faker.Person.FullName,
                Description = $"Ticket #{interaction.TicketNumber} created: {interaction.TicketTitle}"
            });

            // Get existing ticket actions if any
            var ticketActions = interaction.TicketActions?.ToList() ?? new List<TicketAction>();

            // Create intermediate history entries
            for (int i = 0; i < entriesCount - 1; i++)
            {
                // Hours offset progresses with each entry
                var hoursOffset = faker.Random.Double(1, 8 * (i + 1));
                var historyDate = baseDate.AddHours(hoursOffset);

                // If we have an action at a similar time, use that action info
                var relatedAction = ticketActions
                    .OrderBy(a => Math.Abs((a.TakenAt?.Ticks ?? a.CreatedAt.Ticks) - historyDate.Ticks))
                    .FirstOrDefault();

                string description;
                string creator;

                if (relatedAction != null)
                {
                    // Use related action details
                    description = GetHistoryDescriptionFromAction(relatedAction);
                    creator = GetNameFromMail(relatedAction.TakenBy ?? relatedAction.CreatedBy) ?? faker.Person.FullName;
                }
                else
                {
                    // Create a random history entry
                    description = GetRandomHistoryDescription(faker, interaction);
                    creator = faker.Random.Bool(0.7f) ?
                        $"{faker.Person.FullName}" :
                        $"{new Faker().Person.FullName}";
                }

                interaction.TicketHistories.Add(new TicketHistory
                {
                    UserInteractionId = interaction.Id,
                    CreatedAt = historyDate,
                    CreatedBy = creator,
                    Description = description
                });
            }

            // If ticket is closed, add closing history
            if (interaction.Status == "Closed" || interaction.ExternalStatus == "Closed")
            {
                var closedDate = interaction.ExternalDateClosed ?? baseDate.AddHours(faker.Random.Double(24, 72));

                interaction.TicketHistories.Add(new TicketHistory
                {
                    UserInteractionId = interaction.Id,
                    CreatedAt = closedDate,
                    CreatedBy = $"{faker.Person.FullName}",
                    Description = $"Ticket #{interaction.TicketNumber} closed: {faker.PickRandom("Issue resolved", "Solution provided", "Customer confirmed fix")}"
                });
            }

            // Sort all entries by date
            interaction.TicketHistories = interaction.TicketHistories
                .OrderBy(h => h.CreatedAt)
                .ToList();
        }

        private static string GetHistoryDescriptionFromAction(TicketAction action)
        {
            return action.Type switch
            {
                "Created" => $"Ticket created: {action.Description?.Substring(0, Math.Min(action.Description.Length, 50)) ?? "New support request"}",
                "Assigned" => $"Ticket assigned to {action.TakenBy ?? "support team"}",
                "StatusChange" => $"Status changed: {action.Description ?? "Updated ticket status"}",
                "Comment" => $"New comment added{(action.IsPublic ?? false ? " (public)" : " (internal)")}",
                "AttachmentAdded" => "New attachment uploaded to ticket",
                "Closed" => $"Ticket closed: {action.Description?.Substring(0, Math.Min(action.Description.Length, 50)) ?? "Issue resolved"}",
                _ => action.Description?.Substring(0, Math.Min(action.Description.Length, 100)) ?? "Ticket updated"
            };
        }

        private static string GetRandomHistoryDescription(Faker faker, UserInteraction interaction)
        {
            string[] possibleHistoryActions = new[]
            {
            "Status updated to {0}",
            "Priority changed to {0}",
            "Added comment: {0}",
            "Customer contacted via {0}",
            "Assigned to {0}",
            "Reassigned from {0} to {1}",
            "Added note: {0}",
            "{0} field updated",
            "Linked to knowledge base article {0}",
            "Customer responded: {0}"
        };

            string[] statuses = new[] { "Open", "Pending", "In Progress", "Awaiting Customer", "Resolved" };
            string[] priorities = new[] { "Low", "Normal", "High", "Critical" };
            string[] contactMethods = new[] { "email", "phone", "chat", "portal" };
            string[] departments = new[] { "Level 1 Support", "Level 2 Support", "Engineering", "Product Management" };
            string[] fields = new[] { "description", "category", "customer information", "reproduction steps", "environment" };

            string template = faker.PickRandom(possibleHistoryActions);

            return template switch
            {
                "Status updated to {0}" => string.Format(template, faker.PickRandom(statuses)),
                "Priority changed to {0}" => string.Format(template, faker.PickRandom(priorities)),
                "Added comment: {0}" => string.Format(template, faker.Lorem.Sentence()),
                "Customer contacted via {0}" => string.Format(template, faker.PickRandom(contactMethods)),
                "Assigned to {0}" => string.Format(template, faker.PickRandom(departments)),
                "Reassigned from {0} to {1}" => string.Format(template, faker.PickRandom(departments), faker.PickRandom(departments)),
                "Added note: {0}" => string.Format(template, faker.Lorem.Sentence()),
                "{0} field updated" => string.Format(template, faker.PickRandom(fields)),
                "Linked to knowledge base article {0}" => string.Format(template, $"KB-{faker.Random.Number(1000, 9999)}"),
                "Customer responded: {0}" => string.Format(template, faker.Lorem.Sentence()),
                _ => faker.Lorem.Sentence()
            };
        }

        public static void AddRandomTicketHistoriesToInteractions(IEnumerable<UserInteraction> interactions, int minEntries = 2, int maxEntries = 5)
        {
            foreach (var interaction in interactions.Where(i => i.InteractionType == "Ticket"))
            {
                AddRandomTicketHistoriesToInteraction(interaction, minEntries, maxEntries);
            }
        }

        private static string GetNameFromMail(string email)
        {
            string[] parts = email.Split('@');
            if (parts.Length > 0)
            {
                string namePart = parts[0];
                string[] nameParts = namePart.Split('.');
                return string.Join(" ", nameParts.Select(p => char.ToUpper(p[0]) + p.Substring(1)));
            }

            return email;
        }
    }

    public static class RuleResultReasonGenerator
    {
        public static Dictionary<string, List<string>> GetRuleReasons()
        {
            return new Dictionary<string, List<string>>
            {
                // Mistake Analysis Reasons
                ["mistake_analysis"] = new List<string>
            {
                "Agent provided incorrect product information to customer",
                "Troubleshooting steps were not followed according to protocol",
                "Agent failed to verify customer's identity before providing account details",
                "Critical information was missing from ticket documentation",
                "Agent promised a resolution timeline that violates SLA guidelines",
                "Customer was given contradictory information across interactions",
                "Agent neglected to check knowledge base for known issue",
                "Escalation protocol was not followed for critical issue",
                "Agent failed to document workaround provided to customer",
                "Security protocol breach - sensitive data shared in unsecured channel",
                "Agent didn't verify customer's entitlement to premium support",
                "Incorrect categorization of ticket priority",
                "Required fields were not completed in ticket",
                "Customer communication contained grammatical errors affecting clarity",
                "Agent failed to acknowledge previous interactions on this issue"
            },

                // Sentiment Analysis Reasons
                ["sentiment_analysis"] = new List<string>
            {
                "Agent maintained positive tone throughout challenging interaction",
                "Customer expressed high satisfaction with resolution approach",
                "Agent used empathetic language when addressing customer frustration",
                "Communication contained multiple negative sentiment indicators",
                "Agent failed to acknowledge customer's expressed frustration",
                "Resolution was technically correct but delivered with dismissive tone",
                "Agent successfully de-escalated emotionally charged interaction",
                "Customer's negative sentiment transitioned to positive by interaction end",
                "Agent used appropriate reassurance techniques during system outage discussion",
                "Communication lacked warmth in response to customer's expressed concern",
                "Agent maintained professional tone despite customer hostility",
                "Response contained minimal acknowledgment of customer's urgency",
                "Agent effectively used positive language to frame limitations",
                "Customer expressed appreciation multiple times in conversation",
                "Tone remained neutral and professional throughout complex explanation"
            },

                // Semantic Analysis Reasons
                ["semantic_analysis"] = new List<string>
            {
                "Root cause of issue correctly identified in first response",
                "Agent accurately interpreted customer's technical description",
                "Solution provided directly addressed the customer's primary concern",
                "Agent misunderstood key aspects of customer's reported issue",
                "Technical explanation was accurate but excessively complex for customer's level",
                "Agent correctly identified underlying issue not explicitly mentioned by customer",
                "Response addressed symptoms rather than root cause of issue",
                "Agent correctly connected current issue to customer's previous ticket",
                "Troubleshooting approach was logically structured and comprehensive",
                "Agent misinterpreted customer's business requirements",
                "Technical advice was accurate but incomplete for the scenario",
                "Agent correctly prioritized critical issues when multiple were reported",
                "Response missed important contextual information provided by customer",
                "Agent correctly anticipated follow-up questions in initial response",
                "Explanation effectively translated technical concepts to business impact"
            }
            };
        }

        public static string GetRandomReason(string analysisType)
        {
            var reasons = GetRuleReasons();
            if (reasons.ContainsKey(analysisType))
            {
                return reasons[analysisType][new Random().Next(reasons[analysisType].Count)];
            }
            return "No specific reason provided";
        }
    }

    public static class RuleResultCommentGenerator
    {

        public static Dictionary<string, Dictionary<string, List<string>>> GetCommentsByType()
        {
            return new Dictionary<string, Dictionary<string, List<string>>>
            {
                // Mistake Analysis Comments
                ["mistake_analysis"] = new Dictionary<string, List<string>>
                {
                    ["Pass"] = new List<string>
                {
                    "No mistakes identified in the interaction",
                    "Followed all procedures correctly",
                    "Documentation complete and accurate",
                    "Agent provided correct information",
                    "Proper protocols were followed"
                },
                    ["Fail"] = new List<string>
                {
                    "Critical information missing from documentation",
                    "Incorrect solution provided to customer",
                    "Security protocol violated",
                    "SLA commitment made without authorization",
                    "Missed essential troubleshooting steps"
                }
                },

                // Sentiment Analysis Comments
                ["sentiment_analysis"] = new Dictionary<string, List<string>>
                {
                    ["Positive"] = new List<string>
                {
                    "Excellent tone and empathy demonstrated",
                    "Customer satisfaction clearly evident",
                    "Agent maintained positive atmosphere",
                    "Professional and courteous communication",
                    "Effective emotional handling of frustrated customer"
                },
                    ["Neutral"] = new List<string>
                {
                    "Standard professional communication",
                    "Neither positive nor negative sentiment detected",
                    "Transaction-focused with appropriate tone",
                    "Maintained objectivity throughout interaction",
                    "Professionally distant but appropriate"
                },
                    ["Negative"] = new List<string>
                {
                    "Dismissive tone detected in response",
                    "Failed to acknowledge customer frustration",
                    "Communication lacks empathy",
                    "Technical but cold interaction style",
                    "Customer left dissatisfied based on tone"
                }
                },

                // Semantic Analysis Comments
                ["semantic_analysis"] = new Dictionary<string, List<string>>
                {
                    ["Accurate"] = new List<string>
                {
                    "Precise understanding of customer issue",
                    "Root cause correctly identified",
                    "Agent properly interpreted technical requirements",
                    "Thorough and accurate problem assessment",
                    "Correctly prioritized multiple reported issues"
                },
                    ["Inaccurate"] = new List<string>
                {
                    "Misunderstood core customer issue",
                    "Solution addressed symptoms not cause",
                    "Technical requirements incorrectly interpreted",
                    "Failed to recognize critical business impact",
                    "Missed important context from customer"
                }
                }
            };
        }

        public static string GetRandomComment(string analysisType, string outcome)
        {
            var commentsByType = GetCommentsByType();

            // If we have comments for this analysis type and outcome
            if (commentsByType.ContainsKey(analysisType) &&
                commentsByType[analysisType].ContainsKey(outcome))
            {
                var comments = commentsByType[analysisType][outcome];
                return comments[new Random().Next(comments.Count)];
            }

            // Fallback for generic comments
            var genericComments = new List<string>
        {
            "Review completed",
            "Standard quality check performed",
            "Analysis complete",
            $"{outcome}: See details in analysis",
            "Requires additional review"
        };

            return genericComments[new Random().Next(genericComments.Count)];
        }
    }
}
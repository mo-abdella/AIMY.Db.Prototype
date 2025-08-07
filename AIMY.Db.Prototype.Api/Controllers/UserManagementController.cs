//using AIMY.Db.Prototype.Api.Infrastructure;
//using AIMY.Db.Prototype.Infrastructure.Context;
//using AIMY.Db.Prototype.Infrastructure.Entities;
//using Bogus;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Migrations;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using static Amazon.S3.Util.S3EventNotification;

//namespace AIMY.Db.Prototype.Api.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class UserManagementController(MyDbContext context) : ControllerBase
//{
//    [HttpPost("GenerateUsers")]
//    public async Task<IActionResult> GenerateUsers([FromBody] int numberOfUsers)
//    {
//        try
//        {

//            var mohamed = context.Users.ExecuteDelete();

//            var mohamedId = UUIDv7Generator.NewUuid();
//            var userData = new List<User>()
//            {
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "ahmed.mahfouz@flairstech.com", Name = "Ahmed Mahfouz", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "ahmed.youssef@flairstech.com", Name = "Ahmed Hashem Abdelghani Youssef", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "salma.ehab@flairstech.com", Name = "Salma Ehab", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "marina.tawfik@flairstech.com", Name = "Marina Tawfik", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "abduallah.samir@flairstech.com", Name = "Abduallah Samir Hamad Allah Abdelrahman", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "shaimaa.sayed@flairstech.com", Name = "Shaimaa Fawzy Sayed", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "abdo.nasser@flairstech.com", Name = "Abdo Nasser Abdo Ahmed", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "mohamed.abdelaty@flairstech.com", Name = "Mohamed Tarek Abdelaty", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "ahmed.mah66fouz@flairstech.com", Name = "Ahmed Mahfouz", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "mennatalla.osman@flairstech.com", Name = "Mennatalla Mostafa Hussein Osman", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "mahmoud.khalifa@flairstech.com", Name = "Mahmoud Mekawy Khalifa", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "mohamed.shokry@flairstech.com", Name = "Mohamed Hani Shokry", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "mohamed.fadl@flairstech.com", Name = "Mohamed Nabil Fadl", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "passant.azkalani@flairstech.com", Name = "Passant Waleed Azkalani", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "aya.abozeid@flairstech.com", Name = "Aya Abozeid", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "omar.nawar@flairstech.com", Name = "Omar Nawar", CreatedBy = mohamedId },
//                new() { Id = mohamedId, Email = "mohamed.abdella@flairstech.com", Name = "Mohamed Hassan Ahmed Abdella", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "clara.wagdy@flairstech.com", Name = "Clara Wagdy Samuel", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "engy.yasser@flairstech.com", Name = "Engy Yasser Elsayed Elsayed", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "ahmed.elassi@flairstech.com", Name = "Ahmed Elassi", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "habeba.kamel@flairstech.com", Name = "Habeba Kamel", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "mostafa.hassan@flairstech.com", Name = "Mostafa Hassan", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "nadine.elsayed@flairstech.com", Name = "Nadine El Sayed", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "samaa.magdy@flairstech.com", Name = "Samaa Mohamed Magdy", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "samatorab@gmail.com", Name = "Samaa Torab", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "abdelrahman.fathy@flairstech.com", Name = "Abdelrahman Mohamed Fathy Tawfik", CreatedBy = mohamedId },
//                new() { Id = UUIDv7Generator.NewUuid(), Email = "esraa.behery@flairstech.com", Name = "Esraa Behery Gafar Ahmed Shaheen", CreatedBy = mohamedId }
//            };

//            // First, drop the unique constraint on email
//            var result1 = context.Database.ExecuteSql($"ALTER TABLE \"auth\".users DROP CONSTRAINT IF EXISTS users_email_key;");

//            // Now drop the index on email (which should work since the constraint has been removed)
//            var result2 = context.Database.ExecuteSql($"DROP INDEX IF EXISTS \"auth\".idx_users_email;");


//            // Insert the user data
//            context.Users.AddRange(userData);
//            await context.SaveChangesAsync();

//            // Recreate the indexes
//            // First, recreate the regular index
//            var result3 = context.Database.ExecuteSql($"CREATE INDEX idx_users_email ON \"auth\".users (email);");

//            // Then recreate the unique constraint (which will automatically create a unique index)
//            var result4 = context.Database.ExecuteSql($"ALTER TABLE \"auth\".users ADD CONSTRAINT users_email_key UNIQUE (email);");

//            return Ok(new
//            {
//                Message = $"{userData.Count} users generated successfully.",
//                Users = userData.Select(u => new { u.Id, u.Email, u.Name }).ToList()
//            });
//        }
//        catch (Exception ex)
//        {
//            // Return error details for troubleshooting
//            return StatusCode(500, new
//            {
//                Error = "An error occurred while generating users",
//                Message = ex.Message,
//                InnerException = ex.InnerException?.Message
//            });
//        }
//    }


//    [HttpPost("CreateSuperAdminRole")]
//    public async Task<IActionResult> CreateSuperAdminRole(Guid appId)
//    {
//        try
//        {
//            var allRulesDeleted = context.Roles.ExecuteDelete();

//            var superAdminRole = new Role
//            {
//                Id = UUIDv7Generator.NewUuid(),
//                Name = "Super Admin",
//                Key = "super_admin",
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow,
//                CreatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for creation
//                UpdatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12") // Assuming a system user ID for update
//            };

//            context.Roles.Add(superAdminRole);

//            await context.SaveChangesAsync();


//            return Ok(new { Message = "Super Admin role created successfully", RoleId = superAdminRole.Id });
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new { Error = "An error occurred while creating the Super Admin role", Message = ex.Message });
//        }
//    }

//    [HttpPost("CreateAIMYQAApp")]
//    public async Task<IActionResult> CreateAIMYQAApp()
//    {
//        try
//        {
//            var allRolesDeleted = context.Roles.ExecuteDelete();
//            var allAppsDeleted = context.Apps.ExecuteDelete();

//            var app = new App
//            {
//                Id = UUIDv7Generator.NewUuid(), // Assuming a system app ID
//                Name = "AIMY QA",
//                Key = "aimy_qa",
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow,
//                CreatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for creation
//                UpdatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12") // Assuming a system user ID for update
//            };


//            // Drop the unique constraint on app key if it exists
//            var dropConstraintResult = context.Database.ExecuteSql($"ALTER TABLE \"app\".apps DROP CONSTRAINT IF EXISTS apps_key_key;");
//            // Drop the unique constraint on app key if it exists
//            var dropConstraintNameResult = context.Database.ExecuteSql($"ALTER TABLE \"app\".apps DROP CONSTRAINT IF EXISTS apps_name_key;");

//            // Add the app to the context and save changes
//            context.Apps.Add(app);
//            await context.SaveChangesAsync();

//            // Recreate the unique index on app key
//            var createIndexResult = context.Database.ExecuteSql($"CREATE UNIQUE INDEX apps_key_key ON \"app\".apps (key);");
//            // Recreate the unique index on app name
//            var createIndexNameResult = context.Database.ExecuteSql($"CREATE UNIQUE INDEX apps_name_key ON \"app\".apps (name);");

//            return Ok(new { Message = "AIMY QA app created successfully", AppId = app.Id });
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new { Error = "An error occurred while creating the AIMY QA app", Message = ex.Message });
//        }
//    }


//    [HttpPost("AssginSuperAdminRoleToUsers")]
//    public async Task<IActionResult> AssginSuperAdminRoleToUsers()
//    {
//        try
//        {
//            // Get the Super Admin role
//            var superAdminRole = await context.Roles.FirstOrDefaultAsync(r => r.Key == "super_admin");
//            if (superAdminRole == null)
//            {
//                return NotFound(new { Message = "Super Admin role not found" });
//            }
//            // Get all users in the system
//            var users = await context.Users.ToListAsync();
//            // Create UserRole entries for each user
//            var userRoles = users.Select(user => new UserRole
//            {
//                Id = UUIDv7Generator.NewUuid(),
//                UserId = user.Id,
//                AppRoleId = superAdminRole.Id,
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow,
//                CreatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for creation
//                UpdatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12") // Assuming a system user ID for update
//            }).ToList();
//            context.UserRoles.AddRange(userRoles);
//            await context.SaveChangesAsync();
//            return Ok(new { Message = "Super Admin role assigned to all users successfully" });
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new { Error = "An error occurred while assigning the Super Admin role to users", Message = ex.Message });
//        }
//    }

//    [HttpPost("SeedPermissionsModulesAndPermissions")]
//    public async Task<IActionResult> SeedPermissionsModulesAndPermissions()
//    {
//        var aimyQaApp = await context.Apps.FirstOrDefaultAsync(a => a.Key == "aimy_qa");
//        var appId = aimyQaApp.Id;
//        //new Guid("01985523-88c1-70cc-ad87-d458dc700ded");
//        var mohamedAbdella = await context.Users.FirstOrDefaultAsync(u => u.Email == "mohamed.abdella@flairstech.com");
//        var systemUserId = mohamedAbdella.Id;
//        //new Guid("01985511-5e90-7358-8978-ec052554aa12");
//        var now = DateTime.UtcNow;

//        var dashboardModuleId = UUIDv7Generator.NewUuid();
//        var ticketsTableModuleId = UUIDv7Generator.NewUuid();
//        var ticketDetailsModuleId = UUIDv7Generator.NewUuid();
//        var callsTableModuleId = UUIDv7Generator.NewUuid();
//        var callDetailsModuleId = UUIDv7Generator.NewUuid();
//        var manageRulesModuleId = UUIDv7Generator.NewUuid();
//        var manageCallsModuleId = UUIDv7Generator.NewUuid();

//        var modules = new List<PermissionModule>
//        {
//            new() { Id = dashboardModuleId, Name = "Dashboard", AppId = appId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = ticketsTableModuleId, Name = "Tickets Table", AppId = appId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = ticketDetailsModuleId, Name = "Ticket Details", AppId = appId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = callsTableModuleId, Name = "Calls Table", AppId = appId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = callDetailsModuleId, Name = "Call Details", AppId = appId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = manageRulesModuleId, Name = "Manage Rules", AppId = appId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = manageCallsModuleId, Name = "Manage Calls", AppId = appId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now }
//        };

//        var permissions = new List<AppPermission>
//        {
//            // Dashboard Module Permissions
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Quality Score Chart", Resource = "Quality Score Chart", Action = "View", AppId = appId, PermissionModuleId = dashboardModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View CSAT Chart", Resource = "CSAT Chart", Action = "View", AppId = appId, PermissionModuleId = dashboardModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Sentiment Analysis Chart", Resource = "Sentiment Analysis Chart", Action = "View", AppId = appId, PermissionModuleId = dashboardModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Mistake Analysis Chart", Resource = "Mistake Analysis Chart", Action = "View", AppId = appId, PermissionModuleId = dashboardModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Ticket Evaluation", Resource = "Ticket Evaluation", Action = "View", AppId = appId, PermissionModuleId = dashboardModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Insights", Resource = "Insights", Action = "View", AppId = appId, PermissionModuleId = dashboardModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Export Dashboard to PDF", Resource = "Dashboard", Action = "Export to PDF", AppId = appId, PermissionModuleId = dashboardModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },

//            // Tickets Table Module Permissions
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Tickets Table", Resource = "Tickets Table", Action = "View", AppId = appId, PermissionModuleId = ticketsTableModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Export Tickets to CSV Button", Resource = "Tickets", Action = "Export to CSV", AppId = appId, PermissionModuleId = ticketsTableModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },

//            // Ticket Details Module Permissions
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Ticket Details", Resource = "Ticket Details", Action = "View", AppId = appId, PermissionModuleId = ticketDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Ticket Information", Resource = "Ticket Information", Action = "View", AppId = appId, PermissionModuleId = ticketDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View KPI Audit", Resource = "KPI Audit", Action = "View", AppId = appId, PermissionModuleId = ticketDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Edit KPI List", Resource = "KPI List", Action = "Edit", AppId = appId, PermissionModuleId = ticketDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Send Evaluation", Resource = "Evaluation", Action = "Send", AppId = appId, PermissionModuleId = ticketDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Extract Report", Resource = "Report", Action = "Extract", AppId = appId, PermissionModuleId = ticketDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Issue Reported", Resource = "Issue Reported", Action = "View", AppId = appId, PermissionModuleId = ticketDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Ticket Audit Log", Resource = "Ticket Audit Log", Action = "View", AppId = appId, PermissionModuleId = ticketDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Ticket Actions", Resource = "Ticket Actions", Action = "View", AppId = appId, PermissionModuleId = ticketDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },

//            // Calls Table Module Permissions
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Calls Table", Resource = "Calls Table", Action = "View", AppId = appId, PermissionModuleId = callsTableModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Export Calls to CSV Button", Resource = "Calls", Action = "Export to CSV", AppId = appId, PermissionModuleId = callsTableModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },

//            // Call Details Module Permissions
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Call Details", Resource = "Call Details", Action = "View", AppId = appId, PermissionModuleId = callDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Call Information", Resource = "Call Information", Action = "View", AppId = appId, PermissionModuleId = callDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View KPI Audit", Resource = "KPI Audit", Action = "View", AppId = appId, PermissionModuleId = callDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Edit KPI List", Resource = "KPI List", Action = "Edit", AppId = appId, PermissionModuleId = callDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Send Evaluation", Resource = "Evaluation", Action = "Send", AppId = appId, PermissionModuleId = callDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Extract Report", Resource = "Report", Action = "Extract", AppId = appId, PermissionModuleId = callDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Issue Reported", Resource = "Issue Reported", Action = "View", AppId = appId, PermissionModuleId = callDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Call Transcription", Resource = "Call Transcription", Action = "View", AppId = appId, PermissionModuleId = callDetailsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },

//            // Manage Rules Module Permissions
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "View Rules List", Resource = "Rules List", Action = "View", AppId = appId, PermissionModuleId = manageRulesModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Create Rule", Resource = "Rule", Action = "Create", AppId = appId, PermissionModuleId = manageRulesModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Edit Rule", Resource = "Rule", Action = "Edit", AppId = appId, PermissionModuleId = manageRulesModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Delete Rule", Resource = "Rule", Action = "Delete", AppId = appId, PermissionModuleId = manageRulesModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now },

//            // Manage Calls Module Permissions
//            new() { Id = UUIDv7Generator.NewUuid(), PermissionName = "Upload Call", Resource = "Call", Action = "Upload", AppId = appId, PermissionModuleId = manageCallsModuleId, CreatedBy = systemUserId, UpdatedBy = systemUserId, CreatedAt = now, UpdatedAt = now }
//        };

//        // Use a transaction for atomicity
//        using var transaction = await context.Database.BeginTransactionAsync();
//        try
//        {
//            context.PermissionModules.AddRange(modules);
//            context.AppPermissions.AddRange(permissions);
//            await context.SaveChangesAsync();
//            await transaction.CommitAsync();
//            return Ok(new
//            {
//                Message = "Seeded permission modules and permissions successfully.",
//                ModuleIds = modules.Select(m => m.Id).ToList(),
//                PermissionIds = permissions.Select(p => p.Id).ToList()
//            });
//        }
//        catch (Exception ex)
//        {
//            await transaction.RollbackAsync();
//            return StatusCode(500, new { Error = "An error occurred while seeding permissions.", Message = ex.Message });
//        }
//    }


//    [HttpGet("GetPermissionsNames")]
//    public async Task<IActionResult> GetPermissionsNames()
//    {
//        try
//        {
//            var permissions = await context.AppPermissions
//                .Select(p => new { Module = p.PermissionModule.Name, p.PermissionName, p.Resource, p.Action })
//                .ToListAsync();
//            return Ok(permissions);
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new { Error = "An error occurred while retrieving permissions.", Message = ex.Message });
//        }
//    }


//    [HttpPost("SeedAllPermissionsToSuperAdminForAIMYQAApp")]
//    public async Task<IActionResult> AssignAllPermissionsToSuperAdminForAIMYQAApp(CancellationToken cancellationToken)
//    {
//        var appId = (await context.Apps.FirstOrDefaultAsync(a => a.Key == "aimy_qa", cancellationToken))?.Id;

//        var adminRoles = await context.Roles
//            .Where(r => r.Key == "admin")
//            .Include(a => a.AppRolesPermissions)
//            .ThenInclude(a => a.AppPermission)
//            .ToListAsync(cancellationToken);

//        var allPermissions = await context.AppPermissions
//            .Where(p => p.AppId == appId).ToListAsync(cancellationToken);

//        foreach (var role in adminRoles)
//        {
//            role.AppRolesPermissions = allPermissions.Select(a => new AppRolesPermission
//            {
//                Id = UUIDv7Generator.NewUuid(),
//                AppPermissionId = a.Id,
//                AppRoleId = role.Id,
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow,
//                CreatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for creation
//                UpdatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12") // Assuming a system user ID for update

//            }).ToList();
//        }

//        var result = await context.SaveChangesAsync(cancellationToken);

//        if (result > 0)
//        {
//            return Ok(new { Message = "All permissions assigned to Super Admin role successfully." });
//        }
//        else
//        {
//            return StatusCode(500, new { Error = "Failed to assign permissions to Super Admin role." });
//        }
//    }

//    [HttpPost("SeedRoles")]
//    public async Task<IActionResult> SeedRoles()
//    {
//        var rolesDb = await context.Roles.ToListAsync();

//        try
//        {
//            var roles = new List<Role>();
//            for (int i = 1; i <= 20; i++)
//            {
//                roles.Add(new Role
//                {
//                    Id = UUIDv7Generator.NewUuid(),
//                    Name = $"Role {i}",
//                    Key = $"role_{i}",
//                    CreatedAt = DateTime.UtcNow,
//                    UpdatedAt = DateTime.UtcNow,
//                    CreatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"),
//                    UpdatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12")
//                });
//            }
//            context.Roles.AddRange(roles);
//            await context.SaveChangesAsync();
//            return Ok(new { Message = "20 roles seeded successfully (excluding Super Admin and Admin)." });
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new { Error = "An error occurred while seeding roles.", Message = ex.Message });
//        }
//    }


//    [HttpPost("SeedAppPermissions")]
//    public async Task<IActionResult> SeedAppPermissions()
//    {
//        var AimyQaApp = await context.Apps.FirstOrDefaultAsync(a => a.Key == "aimy_qa");

//        var dummyRoles = await context.Roles
//            .Where(r => r.Key.Contains("role_"))
//            .ToListAsync();


//        foreach (var role in dummyRoles)
//        {
//            var moduleId = UUIDv7Generator.NewUuid();
//            var roleModule = new PermissionModule
//            {
//                Id = moduleId,
//                Name = $"Role Module for {role.Name}",
//                AppId = AimyQaApp.Id,
//                CreatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for creation
//                UpdatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for update
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow,
//            };

//            var permissionForThatRoleAndModule = new AppPermission
//            {
//                Id = UUIDv7Generator.NewUuid(),
//                PermissionName = $"View {role.Name} Data",
//                Resource = "Role Data",
//                Action = "View",
//                AppId = AimyQaApp.Id,
//                PermissionModule = roleModule,
//                CreatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for creation
//                UpdatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for update
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow,
//            };

//            role.AppRolesPermissions.Add(new AppRolesPermission
//            {
//                Id = UUIDv7Generator.NewUuid(),
//                AppPermission = permissionForThatRoleAndModule,
//                AppRoleId = role.Id,
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow,
//                CreatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for creation
//                UpdatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12") // Assuming a system user ID for update
//            });

//        }
//        var result = await context.SaveChangesAsync();

//        if (result > 0)
//        {
//            return Ok(new { Message = "App permissions seeded successfully." });
//        }
//        else
//        {
//            return StatusCode(500, new { Error = "Failed to seed app permissions." });
//        }
//    }
//}
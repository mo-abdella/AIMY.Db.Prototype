using AIMY.Db.Prototype.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIMY.Db.Prototype.Infrastructure.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnalysisRule> AnalysisRules { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ExpectedOutput> ExpectedOutputs { get; set; }

    public virtual DbSet<FlywaySchemaHistory> FlywaySchemaHistories { get; set; }

    public virtual DbSet<InteractionAnalysisRuleResult> InteractionAnalysisRuleResults { get; set; }

    public virtual DbSet<InteractionExpectedOutputResult> InteractionExpectedOutputResults { get; set; }

    public virtual DbSet<InteractionReport> InteractionReports { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductUser> ProductUsers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TeamSupportTicket> TeamSupportTickets { get; set; }

    public virtual DbSet<TicketAction> TicketActions { get; set; }

    public virtual DbSet<TicketHistory> TicketHistories { get; set; }

    public virtual DbSet<Tool> Tools { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInteraction> UserInteractions { get; set; }

    public virtual DbSet<UserInteractionEvent> UserInteractionEvents { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<ZendeskTicket> ZendeskTickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnalysisRule>(builder =>
        {
            builder.HasKey(e => e.Id).HasName("analysis_rules_pkey");

            builder.ToTable("analysis_rules", "rule");

            builder.HasIndex(e => e.AnalysisRuleId, "idx_analysis_rule_id");

            builder.HasIndex(e => e.Name, "idx_analysis_rules_name");


            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.AnalysisRuleId).HasColumnName("analysis_rule_id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            builder.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            builder.Property(e => e.Description)
                .HasMaxLength(5000)
                .HasColumnName("description");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            builder.Property(e => e.RuleInteractionType)
                .HasMaxLength(50)
                .HasColumnName("rule_interaction_type");
            builder.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
            builder.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            builder.Property(e => e.Weight)
                .HasPrecision(4, 2)
                .HasColumnName("weight");

            builder.HasOne(d => d.AnalysisRuleNavigation).WithMany(p => p.InverseAnalysisRuleNavigation)
                .HasForeignKey(d => d.AnalysisRuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("analysis_rules_analysis_rule_id_fkey");

            //entity.Property(e => e.ProductId).HasColumnName("product_id");
            //entity.HasIndex(e => e.ProductId, "idx_analysis_rules_product_id");
            //entity.HasOne(d => d.Product).WithMany(p => p.AnalysisRules)
            //    .HasForeignKey(d => d.ProductId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("analysis_rules_product_id_fkey");
            builder.Ignore(e => e.ProductId);
            builder.Ignore(e => e.Product);
            builder.HasMany(e => e.Products).WithMany(e => e.AnalysisRules);
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_messages_pkey");

            entity.ToTable("chat_messages", "interaction");

            entity.HasIndex(e => e.UserInteractionId, "idx_messages_user_interaction_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.ReceivedBy)
                .HasMaxLength(255)
                .HasColumnName("received_by");
            entity.Property(e => e.SentBy)
                .HasMaxLength(255)
                .HasColumnName("sent_by");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserInteractionId).HasColumnName("user_interaction_id");

            entity.HasOne(d => d.UserInteraction).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.UserInteractionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("chat_messages_user_interaction_id_fkey");
        });

        modelBuilder.Entity<Client>(builder =>
        {
            builder.HasKey(e => e.Id).HasName("clients_pkey");

            builder.ToTable("clients", "product");

            builder.HasIndex(e => new { e.Key, e.OrganizationId }, "clients_key_organization_id_key").IsUnique();

            builder.HasIndex(e => new { e.Name, e.OrganizationId }, "clients_name_organization_id_key").IsUnique();

            builder.HasIndex(e => e.OrganizationId, "idx_clients_organization_id");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            builder.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            builder.Property(e => e.Key)
                .HasMaxLength(255)
                .HasColumnName("key");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            builder.Property(e => e.OrganizationId).HasColumnName("organization_id");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            builder.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            builder.HasOne(d => d.Organization).WithMany(p => p.Clients)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clients_organization_id_fkey");
        });

        modelBuilder.Entity<ExpectedOutput>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("expected_outputs_pkey");

            entity.ToTable("expected_outputs", "rule");

            entity.HasIndex(e => e.AnalysisRuleId, "idx_expected_outputs_analysis_rule_id");

            entity.HasIndex(e => e.Name, "idx_expected_outputs_name");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnalysisRuleId).HasColumnName("analysis_rule_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(5000)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OutputType)
                .HasMaxLength(50)
                .HasColumnName("output_type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.AnalysisRule).WithMany(p => p.ExpectedOutputs)
                .HasForeignKey(d => d.AnalysisRuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("expected_outputs_analysis_rule_id_fkey");
        });

        modelBuilder.Entity<FlywaySchemaHistory>(entity =>
        {
            entity.HasKey(e => e.InstalledRank).HasName("flyway_schema_history_pk");

            entity.ToTable("flyway_schema_history", "events");

            entity.HasIndex(e => e.Success, "flyway_schema_history_s_idx");

            entity.Property(e => e.InstalledRank)
                .ValueGeneratedNever()
                .HasColumnName("installed_rank");
            entity.Property(e => e.Checksum).HasColumnName("checksum");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.ExecutionTime).HasColumnName("execution_time");
            entity.Property(e => e.InstalledBy)
                .HasMaxLength(100)
                .HasColumnName("installed_by");
            entity.Property(e => e.InstalledOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("installed_on");
            entity.Property(e => e.Script)
                .HasMaxLength(1000)
                .HasColumnName("script");
            entity.Property(e => e.Success).HasColumnName("success");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasColumnName("version");
        });

        modelBuilder.Entity<InteractionAnalysisRuleResult>(entity =>
        {
            entity.HasKey(e => new { e.UserInteractionId, e.AnalysisRuleId }).HasName("interaction_analysis_rule_results_pkey");

            entity.ToTable("interaction_analysis_rule_results", "rule");

            entity.HasIndex(e => e.AnalysisRuleId, "idx_interaction_analysis_rule_results_analysis_rule_id");

            entity.HasIndex(e => e.Type, "idx_interaction_analysis_rule_results_type");

            entity.HasIndex(e => e.UserInteractionId, "idx_interaction_analysis_rule_results_user_interaction_id");

            entity.Property(e => e.UserInteractionId).HasColumnName("user_interaction_id");
            entity.Property(e => e.AnalysisRuleId).HasColumnName("analysis_rule_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.QaReview)
                .HasMaxLength(255)
                .HasColumnName("qa_review");
            entity.Property(e => e.QaScore)
                .HasPrecision(10, 2)
                .HasColumnName("qa_score");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Score)
                .HasPrecision(10, 2)
                .HasColumnName("score");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.AnalysisRule).WithMany(p => p.InteractionAnalysisRuleResults)
                .HasForeignKey(d => d.AnalysisRuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("interaction_analysis_rule_results_analysis_rule_id_fkey");

            entity.HasOne(d => d.UserInteraction).WithMany(p => p.InteractionAnalysisRuleResults)
                .HasForeignKey(d => d.UserInteractionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("interaction_analysis_rule_results_user_interaction_id_fkey");
        });

        modelBuilder.Entity<InteractionExpectedOutputResult>(entity =>
        {
            entity.HasKey(e => new { e.UserInteractionId, e.ExpectedOutputId }).HasName("pk_interaction_expected_output_results");

            entity.ToTable("interaction_expected_output_results", "rule");

            entity.HasIndex(e => new { e.UserInteractionId, e.ExpectedOutputId }, "idx_interaction_expected_output_results_composite");

            entity.HasIndex(e => e.OutputType, "idx_interaction_expected_output_results_type");

            entity.Property(e => e.UserInteractionId).HasColumnName("user_interaction_id");
            entity.Property(e => e.ExpectedOutputId).HasColumnName("expected_output_id");
            entity.Property(e => e.AiOrQa).HasColumnName("ai_or_qa");
            entity.Property(e => e.BooleanOutput).HasColumnName("boolean_output");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.NumberOutput)
                .HasPrecision(5, 2)
                .HasColumnName("number_output");
            entity.Property(e => e.OutputType)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("output_type");
            entity.Property(e => e.TextOutput)
                .HasMaxLength(255)
                .HasColumnName("text_output");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.ExpectedOutput).WithMany(p => p.InteractionExpectedOutputResults)
                .HasForeignKey(d => d.ExpectedOutputId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("interaction_expected_output_results_expected_output_id_fkey");

            entity.HasOne(d => d.UserInteraction).WithMany(p => p.InteractionExpectedOutputResults)
                .HasForeignKey(d => d.UserInteractionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("interaction_expected_output_results_user_interaction_id_fkey");
        });

        modelBuilder.Entity<InteractionReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("interaction_report_pkey");

            entity.ToTable("interaction_report", "interaction");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ActionTaken)
                .HasColumnType("jsonb")
                .HasColumnName("action_taken");
            entity.Property(e => e.IssueReported)
                .HasColumnType("jsonb")
                .HasColumnName("issue_reported");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.InteractionReport)
                .HasForeignKey<InteractionReport>(d => d.Id)
                .HasConstraintName("fk_interaction_report_user_interactions_id");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_jobs");

            entity.ToTable("jobs", "job");

            entity.HasIndex(e => e.ProductId, "ix_jobs_product_id");

            entity.HasIndex(e => e.ToolId, "ix_jobs_tool_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.LastRunTime).HasColumnName("last_run_time");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ResultMessage).HasColumnName("result_message");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ToolId).HasColumnName("tool_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Product).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_jobs_products_product_id");

            entity.HasOne(d => d.Tool).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.ToolId)
                .HasConstraintName("fk_jobs_tools_tool_id");
        });

        modelBuilder.Entity<Organization>(builder =>
        {
            builder.HasKey(e => e.Id).HasName("organizations_pkey");

            builder.ToTable("organizations", "product");

            builder.HasIndex(e => e.Key, "organizations_key_key").IsUnique();

            builder.HasIndex(e => e.Name, "organizations_name_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            builder.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            builder.Property(e => e.Key)
                .HasMaxLength(255)
                .HasColumnName("key");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            builder.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Product>(builder =>
        {
            builder.HasKey(e => e.Id).HasName("products_pkey");

            builder.ToTable("products", "product");

            builder.HasIndex(e => e.ClientId, "idx_products_client_id");

            builder.HasIndex(e => new { e.Key, e.ClientId }, "products_key_client_id_key").IsUnique();

            builder.HasIndex(e => new { e.Name, e.ClientId }, "products_name_client_id_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ClientId).HasColumnName("client_id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            builder.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            builder.Property(e => e.Key)
                .HasMaxLength(255)
                .HasColumnName("key");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            builder.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            builder.HasOne(d => d.Client).WithMany(p => p.Products)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_client_id_fkey");
        });

        modelBuilder.Entity<ProductUser>(builder =>
        {
            builder.HasKey(e => new { e.OrganizationId, e.ClientId, e.ProductId, e.UserId }).HasName("product_user_pkey");

            builder.ToTable("product_user", "product");

            builder.HasIndex(e => e.UserId, "idx_product_user_user_id");

            builder.Property(e => e.ProductId).HasColumnName("product_id");
            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            builder.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            builder.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            builder.HasOne(d => d.Product).WithMany(p => p.ProductUsers)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_user_product_id_fkey");

            builder.HasOne(d => d.Client).WithMany(p => p.ProductUsers)
            .HasForeignKey(d => d.ClientId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("product_user_client_id_fkey");

            builder.HasOne(d => d.Organization).WithMany(p => p.ProductUsers)
                .HasForeignKey(d => d.OrganizationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_user_organization_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles", "auth");

            entity.HasIndex(e => e.Name, "roles_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<TeamSupportTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_team_support_tickets");

            entity.ToTable("team_support_tickets", "interaction");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.CallerField)
                .HasMaxLength(255)
                .HasColumnName("caller_field");
            entity.Property(e => e.CreatorName)
                .HasMaxLength(255)
                .HasColumnName("creator_name");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.TimeSpent).HasColumnName("time_spent");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.TeamSupportTicket)
                .HasForeignKey<TeamSupportTicket>(d => d.Id)
                .HasConstraintName("fk_team_support_tickets_user_interactions_id");
        });

        modelBuilder.Entity<TicketAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_actions_pkey");

            entity.ToTable("ticket_actions", "interaction");

            entity.HasIndex(e => e.UserInteractionId, "idx_actions_user_interaction_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Attachments)
                .HasColumnType("jsonb")
                .HasColumnName("attachments");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.HtmlBody).HasColumnName("html_body");
            entity.Property(e => e.IsPublic)
                .HasDefaultValue(false)
                .HasColumnName("is_public");
            entity.Property(e => e.TakenAt).HasColumnName("taken_at");
            entity.Property(e => e.TakenBy)
                .HasMaxLength(255)
                .HasColumnName("taken_by");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserInteractionId).HasColumnName("user_interaction_id");

            entity.HasOne(d => d.UserInteraction).WithMany(p => p.TicketActions)
                .HasForeignKey(d => d.UserInteractionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_actions_user_interaction_id_fkey");
        });

        modelBuilder.Entity<TicketHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_history_pkey");

            entity.ToTable("ticket_history", "interaction");

            entity.HasIndex(e => e.UserInteractionId, "IX_ticket_history_user_interaction_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.UserInteractionId).HasColumnName("user_interaction_id");

            entity.HasOne(d => d.UserInteraction).WithMany(p => p.TicketHistories)
                .HasForeignKey(d => d.UserInteractionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_history_user_interaction_id_fkey");
        });

        modelBuilder.Entity<Tool>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tools_pkey");

            entity.ToTable("tools", "product");

            entity.HasIndex(e => e.AccessType, "idx_access_type");



            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessType)
                .HasMaxLength(255)
                .HasDefaultValueSql("'api'::character varying")
                .HasColumnName("access_type");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(512)
                .HasColumnName("api_key");
            entity.Property(e => e.ApiSecret).HasColumnName("api_secret");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.Url)
                .HasMaxLength(512)
                .HasColumnName("url");

            //entity.HasIndex(e => new { e.Name, e.ProductId }, "tools_name_product_id_key").IsUnique();
            //entity.HasIndex(e => e.ProductId, "idx_tools_product_id");
            //entity.Property(e => e.ProductId).HasColumnName("product_id");
            //entity.HasOne(d => d.Product).WithMany(p => p.Tools)
            //    .HasForeignKey(d => d.ProductId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("tools_product_id_fkey");
            entity.Ignore(e => e.ProductId);
            entity.Ignore(e => e.Product);
            entity.HasMany(d => d.Products).WithMany(p => p.Tools);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users", "auth");

            entity.HasIndex(e => e.Email, "idx_users_email");

            entity.HasIndex(e => e.SupervisorId, "idx_users_supervisor_id");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.SupervisorId).HasColumnName("supervisor_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.InverseSupervisor)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("users_supervisor_id_fkey");
        });

        modelBuilder.Entity<UserInteraction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_interactions_pkey");

            entity.ToTable("user_interactions", "interaction");

            entity.HasIndex(e => e.AgentId, "idx_agent_id");

            entity.HasIndex(e => e.InteractionType, "idx_interaction_type");

            entity.HasIndex(e => e.ToolId, "idx_tool_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgentEmail)
                .HasMaxLength(255)
                .HasColumnName("agent_email");
            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.CallDirection)
                .HasMaxLength(50)
                .HasColumnName("call_direction");
            entity.Property(e => e.CallDuration).HasColumnName("call_duration");
            entity.Property(e => e.CallHoldDuration).HasColumnName("call_hold_duration");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.ExternalDateClosed).HasColumnName("external_date_closed");
            entity.Property(e => e.ExternalDateCreated).HasColumnName("external_date_created");
            entity.Property(e => e.ExternalId)
                .HasMaxLength(255)
                .HasColumnName("external_id");
            entity.Property(e => e.ExternalStatus)
                .HasMaxLength(255)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("external_status");
            entity.Property(e => e.FirstResponseTimeTaken).HasColumnName("first_response_time_taken");
            entity.Property(e => e.InteractionType)
                .HasMaxLength(50)
                .HasColumnName("interaction_type");
            entity.Property(e => e.MistakeAnalysisScoreAi)
                .HasPrecision(4, 2)
                .HasColumnName("mistake_analysis_score_ai");
            entity.Property(e => e.MistakeAnalysisScoreQa)
                .HasPrecision(4, 2)
                .HasColumnName("mistake_analysis_score_qa");
            entity.Property(e => e.SemanticAnalysisAi)
                .HasMaxLength(5000)
                .HasColumnName("semantic_analysis_ai");
            entity.Property(e => e.SemanticAnalysisQa)
                .HasMaxLength(5000)
                .HasColumnName("semantic_analysis_qa");
            entity.Property(e => e.SentimentAnalysisScoreAi)
                .HasPrecision(4, 2)
                .HasColumnName("sentiment_analysis_score_ai");
            entity.Property(e => e.SentimentAnalysisScoreQa)
                .HasPrecision(4, 2)
                .HasColumnName("sentiment_analysis_score_qa");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TextContent).HasColumnName("text_content");
            entity.Property(e => e.TicketDescription).HasColumnName("ticket_description");
            entity.Property(e => e.TicketGroup)
                .HasMaxLength(255)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("ticket_group");
            entity.Property(e => e.TicketNumber)
                .HasMaxLength(255)
                .HasColumnName("ticket_number");
            entity.Property(e => e.TicketTags).HasColumnName("ticket_tags");
            entity.Property(e => e.TicketTitle)
                .HasMaxLength(255)
                .HasColumnName("ticket_title");
            entity.Property(e => e.TicketTypeName)
                .HasMaxLength(50)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("ticket_type_name");
            entity.Property(e => e.ToolId).HasColumnName("tool_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Tool).WithMany(p => p.UserInteractions)
                .HasForeignKey(d => d.ToolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_interactions_tool_id_fkey");
        });

        modelBuilder.Entity<UserInteractionEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_interaction_events_pkey");

            entity.ToTable("user_interaction_events", "events");

            entity.HasIndex(e => e.UserInteractionId, "idx_user_interaction_events_interaction_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.OperationName)
                .HasMaxLength(50)
                .HasColumnName("operation_name");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .HasColumnName("service_name");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserInteractionId).HasColumnName("user_interaction_id");

            entity.HasOne(d => d.UserInteraction).WithMany(p => p.UserInteractionEvents)
                .HasForeignKey(d => d.UserInteractionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_interaction_events_user_interaction_id_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("user_role_pkey");

            entity.ToTable("user_role", "auth");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_role_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_user_id_fkey");
        });

        modelBuilder.Entity<ZendeskTicket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("zendesk_tickets_pkey");

            entity.ToTable("zendesk_tickets", "interaction");

            entity.Property(e => e.TicketId)
                .ValueGeneratedNever()
                .HasColumnName("ticket_id");
            entity.Property(e => e.Assignee).HasColumnName("assignee");
            entity.Property(e => e.Channel).HasColumnName("channel");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FirstResponseTimeTaken).HasColumnName("first_response_time_taken");
            entity.Property(e => e.GeneratedTimestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("generated_timestamp");
            entity.Property(e => e.InternalStatus).HasColumnName("internal_status");
            entity.Property(e => e.JiraTicketId).HasColumnName("jira_ticket_id");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.PublicStatus).HasColumnName("public_status");
            entity.Property(e => e.RequesterId).HasColumnName("requester_id");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.SubmitterId).HasColumnName("submitter_id");
            entity.Property(e => e.Tags).HasColumnName("tags");
            entity.Property(e => e.TicketGroup).HasColumnName("ticket_group");
            entity.Property(e => e.TimeSpent).HasColumnName("time_spent");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.UserInteractionId).HasColumnName("user_interaction_id");

            entity.HasOne(d => d.UserInteraction).WithMany(p => p.ZendeskTickets)
                .HasForeignKey(d => d.UserInteractionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_zendesk_ticket_user_interaction");
        });
    }

}

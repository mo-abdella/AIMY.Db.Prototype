using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AIMY.Db.Prototype.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rule");

            migrationBuilder.EnsureSchema(
                name: "interaction");

            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.EnsureSchema(
                name: "events");

            migrationBuilder.EnsureSchema(
                name: "job");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "analysis_rules",
                schema: "rule",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    weight = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    analysis_rule_id = table.Column<int>(type: "integer", nullable: true),
                    rule_interaction_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("analysis_rules_pkey", x => x.id);
                    table.ForeignKey(
                        name: "analysis_rules_analysis_rule_id_fkey",
                        column: x => x.analysis_rule_id,
                        principalSchema: "rule",
                        principalTable: "analysis_rules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flyway_schema_history",
                schema: "events",
                columns: table => new
                {
                    installed_rank = table.Column<int>(type: "integer", nullable: false),
                    version = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    script = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    checksum = table.Column<int>(type: "integer", nullable: true),
                    installed_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    installed_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    execution_time = table.Column<int>(type: "integer", nullable: false),
                    success = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("flyway_schema_history_pk", x => x.installed_rank);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                schema: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    key = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("organizations_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tools",
                schema: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    url = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    api_key = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    api_secret = table.Column<byte[]>(type: "bytea", nullable: true),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    access_type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, defaultValueSql: "'api'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("tools_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    supervisor_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                    table.ForeignKey(
                        name: "users_supervisor_id_fkey",
                        column: x => x.supervisor_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "expected_outputs",
                schema: "rule",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    output_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    analysis_rule_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("expected_outputs_pkey", x => x.id);
                    table.ForeignKey(
                        name: "expected_outputs_analysis_rule_id_fkey",
                        column: x => x.analysis_rule_id,
                        principalSchema: "rule",
                        principalTable: "analysis_rules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                schema: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    key = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    organization_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clients_pkey", x => x.id);
                    table.ForeignKey(
                        name: "clients_organization_id_fkey",
                        column: x => x.organization_id,
                        principalSchema: "product",
                        principalTable: "organizations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_interactions",
                schema: "interaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    agent_email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    agent_id = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    text_content = table.Column<string>(type: "text", nullable: true),
                    call_duration = table.Column<int>(type: "integer", nullable: true),
                    call_direction = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    call_hold_duration = table.Column<int>(type: "integer", nullable: true),
                    ticket_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ticket_description = table.Column<string>(type: "text", nullable: true),
                    ticket_title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    interaction_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    tool_id = table.Column<int>(type: "integer", nullable: false),
                    sentiment_analysis_score_ai = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true),
                    sentiment_analysis_score_qa = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true),
                    mistake_analysis_score_ai = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true),
                    mistake_analysis_score_qa = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true),
                    semantic_analysis_ai = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    semantic_analysis_qa = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    ticket_group = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true, defaultValueSql: "''::character varying"),
                    ticket_tags = table.Column<List<string>>(type: "text[]", nullable: true),
                    ticket_type_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''::character varying"),
                    first_response_time_taken = table.Column<long>(type: "bigint", nullable: true),
                    external_date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    external_date_closed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    external_status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true, defaultValueSql: "''::character varying"),
                    external_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_interactions_pkey", x => x.id);
                    table.ForeignKey(
                        name: "user_interactions_tool_id_fkey",
                        column: x => x.tool_id,
                        principalSchema: "product",
                        principalTable: "tools",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                schema: "auth",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_role_pkey", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "user_role_role_id_fkey",
                        column: x => x.role_id,
                        principalSchema: "auth",
                        principalTable: "roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "user_role_user_id_fkey",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    key = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("products_pkey", x => x.id);
                    table.ForeignKey(
                        name: "products_client_id_fkey",
                        column: x => x.client_id,
                        principalSchema: "product",
                        principalTable: "clients",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "chat_messages",
                schema: "interaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    sent_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    received_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    user_interaction_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_messages_pkey", x => x.id);
                    table.ForeignKey(
                        name: "chat_messages_user_interaction_id_fkey",
                        column: x => x.user_interaction_id,
                        principalSchema: "interaction",
                        principalTable: "user_interactions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "interaction_analysis_rule_results",
                schema: "rule",
                columns: table => new
                {
                    user_interaction_id = table.Column<int>(type: "integer", nullable: false),
                    analysis_rule_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    score = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    comment = table.Column<string>(type: "text", nullable: true),
                    qa_score = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    qa_review = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("interaction_analysis_rule_results_pkey", x => new { x.user_interaction_id, x.analysis_rule_id });
                    table.ForeignKey(
                        name: "interaction_analysis_rule_results_analysis_rule_id_fkey",
                        column: x => x.analysis_rule_id,
                        principalSchema: "rule",
                        principalTable: "analysis_rules",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "interaction_analysis_rule_results_user_interaction_id_fkey",
                        column: x => x.user_interaction_id,
                        principalSchema: "interaction",
                        principalTable: "user_interactions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "interaction_expected_output_results",
                schema: "rule",
                columns: table => new
                {
                    user_interaction_id = table.Column<int>(type: "integer", nullable: false),
                    expected_output_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ai_or_qa = table.Column<bool>(type: "boolean", nullable: true),
                    output_type = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: false),
                    text_output = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    number_output = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    boolean_output = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_interaction_expected_output_results", x => new { x.user_interaction_id, x.expected_output_id });
                    table.ForeignKey(
                        name: "interaction_expected_output_results_expected_output_id_fkey",
                        column: x => x.expected_output_id,
                        principalSchema: "rule",
                        principalTable: "expected_outputs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "interaction_expected_output_results_user_interaction_id_fkey",
                        column: x => x.user_interaction_id,
                        principalSchema: "interaction",
                        principalTable: "user_interactions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "interaction_report",
                schema: "interaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    issue_reported = table.Column<string>(type: "jsonb", nullable: false),
                    action_taken = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("interaction_report_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_interaction_report_user_interactions_id",
                        column: x => x.id,
                        principalSchema: "interaction",
                        principalTable: "user_interactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_support_tickets",
                schema: "interaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    product_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    caller_field = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    time_spent = table.Column<double>(type: "double precision", nullable: true),
                    creator_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    action = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_team_support_tickets", x => x.id);
                    table.ForeignKey(
                        name: "fk_team_support_tickets_user_interactions_id",
                        column: x => x.id,
                        principalSchema: "interaction",
                        principalTable: "user_interactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket_actions",
                schema: "interaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    taken_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    taken_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    user_interaction_id = table.Column<int>(type: "integer", nullable: false),
                    html_body = table.Column<string>(type: "text", nullable: true),
                    is_public = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    attachments = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ticket_actions_pkey", x => x.id);
                    table.ForeignKey(
                        name: "ticket_actions_user_interaction_id_fkey",
                        column: x => x.user_interaction_id,
                        principalSchema: "interaction",
                        principalTable: "user_interactions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ticket_history",
                schema: "interaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_interaction_id = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ticket_history_pkey", x => x.id);
                    table.ForeignKey(
                        name: "ticket_history_user_interaction_id_fkey",
                        column: x => x.user_interaction_id,
                        principalSchema: "interaction",
                        principalTable: "user_interactions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_interaction_events",
                schema: "events",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    user_interaction_id = table.Column<int>(type: "integer", nullable: false),
                    service_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    operation_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_interaction_events_pkey", x => x.id);
                    table.ForeignKey(
                        name: "user_interaction_events_user_interaction_id_fkey",
                        column: x => x.user_interaction_id,
                        principalSchema: "interaction",
                        principalTable: "user_interactions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "zendesk_tickets",
                schema: "interaction",
                columns: table => new
                {
                    ticket_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    subject = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    internal_status = table.Column<string>(type: "text", nullable: true),
                    public_status = table.Column<string>(type: "text", nullable: true),
                    assignee = table.Column<string>(type: "text", nullable: true),
                    requester_id = table.Column<long>(type: "bigint", nullable: true),
                    submitter_id = table.Column<long>(type: "bigint", nullable: true),
                    organization_id = table.Column<long>(type: "bigint", nullable: true),
                    ticket_group = table.Column<string>(type: "text", nullable: true),
                    priority = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    channel = table.Column<string>(type: "text", nullable: true),
                    first_response_time_taken = table.Column<long>(type: "bigint", nullable: true),
                    time_spent = table.Column<long>(type: "bigint", nullable: true),
                    tags = table.Column<List<string>>(type: "text[]", nullable: true),
                    url = table.Column<string>(type: "text", nullable: true),
                    generated_timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    jira_ticket_id = table.Column<long>(type: "bigint", nullable: true),
                    user_interaction_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("zendesk_tickets_pkey", x => x.ticket_id);
                    table.ForeignKey(
                        name: "fk_zendesk_ticket_user_interaction",
                        column: x => x.user_interaction_id,
                        principalSchema: "interaction",
                        principalTable: "user_interactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "analysis_rule_product",
                schema: "rule",
                columns: table => new
                {
                    analysis_rule_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("analysis_rule_product_pkey", x => new { x.analysis_rule_id, x.product_id });
                    table.ForeignKey(
                        name: "analysis_rule_product_analysis_rule_id_fkey",
                        column: x => x.analysis_rule_id,
                        principalSchema: "rule",
                        principalTable: "analysis_rules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "analysis_rule_product_product_id_fkey",
                        column: x => x.product_id,
                        principalSchema: "product",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "jobs",
                schema: "job",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    last_run_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    result_message = table.Column<string>(type: "text", nullable: true),
                    tool_id = table.Column<int>(type: "integer", nullable: true),
                    product_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jobs", x => x.id);
                    table.ForeignKey(
                        name: "fk_jobs_products_product_id",
                        column: x => x.product_id,
                        principalSchema: "product",
                        principalTable: "products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_jobs_tools_tool_id",
                        column: x => x.tool_id,
                        principalSchema: "product",
                        principalTable: "tools",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product_tool",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: true),
                    tool_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    client_id = table.Column<int>(type: "integer", nullable: true),
                    organization_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_tool_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "product_tool_client_id_fkey",
                        column: x => x.client_id,
                        principalSchema: "product",
                        principalTable: "clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "product_tool_organization_id_fkey",
                        column: x => x.organization_id,
                        principalSchema: "product",
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "product_tool_product_id_fkey",
                        column: x => x.product_id,
                        principalSchema: "product",
                        principalTable: "products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "product_tool_tool_id_fkey",
                        column: x => x.tool_id,
                        principalSchema: "product",
                        principalTable: "tools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_user",
                schema: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: true),
                    client_id = table.Column<int>(type: "integer", nullable: true),
                    organization_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_user_pkey", x => x.id);
                    table.ForeignKey(
                        name: "product_user_client_id_fkey",
                        column: x => x.client_id,
                        principalSchema: "product",
                        principalTable: "clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "product_user_organization_id_fkey",
                        column: x => x.organization_id,
                        principalSchema: "product",
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "product_user_product_id_fkey",
                        column: x => x.product_id,
                        principalSchema: "product",
                        principalTable: "products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "product_user_user_id_fkey",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "idx_analysis_rule_product_product_id",
                schema: "rule",
                table: "analysis_rule_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_analysis_rule_id",
                schema: "rule",
                table: "analysis_rules",
                column: "analysis_rule_id");

            migrationBuilder.CreateIndex(
                name: "idx_analysis_rules_name",
                schema: "rule",
                table: "analysis_rules",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "idx_analysis_rules_product_id",
                schema: "rule",
                table: "analysis_rules",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_messages_user_interaction_id",
                schema: "interaction",
                table: "chat_messages",
                column: "user_interaction_id");

            migrationBuilder.CreateIndex(
                name: "clients_key_organization_id_key",
                schema: "product",
                table: "clients",
                columns: new[] { "key", "organization_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "clients_name_organization_id_key",
                schema: "product",
                table: "clients",
                columns: new[] { "name", "organization_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_clients_organization_id",
                schema: "product",
                table: "clients",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "idx_expected_outputs_analysis_rule_id",
                schema: "rule",
                table: "expected_outputs",
                column: "analysis_rule_id");

            migrationBuilder.CreateIndex(
                name: "idx_expected_outputs_name",
                schema: "rule",
                table: "expected_outputs",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "flyway_schema_history_s_idx",
                schema: "events",
                table: "flyway_schema_history",
                column: "success");

            migrationBuilder.CreateIndex(
                name: "idx_interaction_analysis_rule_results_analysis_rule_id",
                schema: "rule",
                table: "interaction_analysis_rule_results",
                column: "analysis_rule_id");

            migrationBuilder.CreateIndex(
                name: "idx_interaction_analysis_rule_results_type",
                schema: "rule",
                table: "interaction_analysis_rule_results",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "idx_interaction_analysis_rule_results_user_interaction_id",
                schema: "rule",
                table: "interaction_analysis_rule_results",
                column: "user_interaction_id");

            migrationBuilder.CreateIndex(
                name: "idx_interaction_expected_output_results_composite",
                schema: "rule",
                table: "interaction_expected_output_results",
                columns: new[] { "user_interaction_id", "expected_output_id" });

            migrationBuilder.CreateIndex(
                name: "idx_interaction_expected_output_results_type",
                schema: "rule",
                table: "interaction_expected_output_results",
                column: "output_type");

            migrationBuilder.CreateIndex(
                name: "IX_interaction_expected_output_results_expected_output_id",
                schema: "rule",
                table: "interaction_expected_output_results",
                column: "expected_output_id");

            migrationBuilder.CreateIndex(
                name: "ix_jobs_product_id",
                schema: "job",
                table: "jobs",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_jobs_tool_id",
                schema: "job",
                table: "jobs",
                column: "tool_id");

            migrationBuilder.CreateIndex(
                name: "organizations_key_key",
                schema: "product",
                table: "organizations",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "organizations_name_key",
                schema: "product",
                table: "organizations",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_product_tool_client_id",
                schema: "product",
                table: "product_tool",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_tool_organization_id",
                schema: "product",
                table: "product_tool",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_tool_product_id",
                schema: "product",
                table: "product_tool",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_tool_tool_id",
                schema: "product",
                table: "product_tool",
                column: "tool_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_user_client_id",
                schema: "product",
                table: "product_user",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_user_organization_id",
                schema: "product",
                table: "product_user",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_user_user_id",
                schema: "product",
                table: "product_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_user_product_id",
                schema: "product",
                table: "product_user",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_products_client_id",
                schema: "product",
                table: "products",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "products_key_client_id_key",
                schema: "product",
                table: "products",
                columns: new[] { "key", "client_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "products_name_client_id_key",
                schema: "product",
                table: "products",
                columns: new[] { "name", "client_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "roles_name_key",
                schema: "auth",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_actions_user_interaction_id",
                schema: "interaction",
                table: "ticket_actions",
                column: "user_interaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_history_user_interaction_id",
                schema: "interaction",
                table: "ticket_history",
                column: "user_interaction_id");

            migrationBuilder.CreateIndex(
                name: "idx_access_type",
                schema: "product",
                table: "tools",
                column: "access_type");

            migrationBuilder.CreateIndex(
                name: "idx_user_interaction_events_interaction_id",
                schema: "events",
                table: "user_interaction_events",
                column: "user_interaction_id");

            migrationBuilder.CreateIndex(
                name: "idx_agent_id",
                schema: "interaction",
                table: "user_interactions",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "idx_interaction_type",
                schema: "interaction",
                table: "user_interactions",
                column: "interaction_type");

            migrationBuilder.CreateIndex(
                name: "idx_tool_id",
                schema: "interaction",
                table: "user_interactions",
                column: "tool_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_role_id",
                schema: "auth",
                table: "user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "idx_users_email",
                schema: "auth",
                table: "users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "idx_users_supervisor_id",
                schema: "auth",
                table: "users",
                column: "supervisor_id");

            migrationBuilder.CreateIndex(
                name: "users_email_key",
                schema: "auth",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_zendesk_tickets_user_interaction_id",
                schema: "interaction",
                table: "zendesk_tickets",
                column: "user_interaction_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analysis_rule_product",
                schema: "rule");

            migrationBuilder.DropTable(
                name: "chat_messages",
                schema: "interaction");

            migrationBuilder.DropTable(
                name: "flyway_schema_history",
                schema: "events");

            migrationBuilder.DropTable(
                name: "interaction_analysis_rule_results",
                schema: "rule");

            migrationBuilder.DropTable(
                name: "interaction_expected_output_results",
                schema: "rule");

            migrationBuilder.DropTable(
                name: "interaction_report",
                schema: "interaction");

            migrationBuilder.DropTable(
                name: "jobs",
                schema: "job");

            migrationBuilder.DropTable(
                name: "product_tool",
                schema: "product");

            migrationBuilder.DropTable(
                name: "product_user",
                schema: "product");

            migrationBuilder.DropTable(
                name: "team_support_tickets",
                schema: "interaction");

            migrationBuilder.DropTable(
                name: "ticket_actions",
                schema: "interaction");

            migrationBuilder.DropTable(
                name: "ticket_history",
                schema: "interaction");

            migrationBuilder.DropTable(
                name: "user_interaction_events",
                schema: "events");

            migrationBuilder.DropTable(
                name: "user_role",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "zendesk_tickets",
                schema: "interaction");

            migrationBuilder.DropTable(
                name: "expected_outputs",
                schema: "rule");

            migrationBuilder.DropTable(
                name: "products",
                schema: "product");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "users",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "user_interactions",
                schema: "interaction");

            migrationBuilder.DropTable(
                name: "analysis_rules",
                schema: "rule");

            migrationBuilder.DropTable(
                name: "clients",
                schema: "product");

            migrationBuilder.DropTable(
                name: "tools",
                schema: "product");

            migrationBuilder.DropTable(
                name: "organizations",
                schema: "product");
        }
    }
}

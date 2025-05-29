using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIMY.Db.Prototype.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateManytoManyinRuleProductOrganzationTool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "analysis_rules_product_id_fkey",
                schema: "rule",
                table: "analysis_rules");

            migrationBuilder.DropForeignKey(
                name: "product_user_user_id_fkey",
                schema: "product",
                table: "product_user");

            migrationBuilder.DropForeignKey(
                name: "tools_product_id_fkey",
                schema: "product",
                table: "tools");

            migrationBuilder.DropIndex(
                name: "ix_tools_name_product_id",
                schema: "product",
                table: "tools");

            migrationBuilder.DropIndex(
                name: "ix_tools_product_id",
                schema: "product",
                table: "tools");

            migrationBuilder.DropPrimaryKey(
                name: "product_user_pkey",
                schema: "product",
                table: "product_user");

            migrationBuilder.DropIndex(
                name: "ix_analysis_rules_product_id",
                schema: "rule",
                table: "analysis_rules");

            migrationBuilder.DropColumn(
                name: "product_id",
                schema: "product",
                table: "tools");

            migrationBuilder.DropColumn(
                name: "product_id",
                schema: "rule",
                table: "analysis_rules");

            migrationBuilder.AddColumn<int>(
                name: "organization_id",
                schema: "product",
                table: "product_user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "client_id",
                schema: "product",
                table: "product_user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "interaction_type",
                schema: "rule",
                table: "analysis_rules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "product_user_pkey",
                schema: "product",
                table: "product_user",
                columns: new[] { "organization_id", "client_id", "product_id", "user_id" });

            migrationBuilder.CreateTable(
                name: "analysis_rule_product",
                columns: table => new
                {
                    analysis_rules_id = table.Column<int>(type: "integer", nullable: false),
                    products_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_analysis_rule_product", x => new { x.analysis_rules_id, x.products_id });
                    table.ForeignKey(
                        name: "fk_analysis_rule_product_analysis_rules_analysis_rules_id",
                        column: x => x.analysis_rules_id,
                        principalSchema: "rule",
                        principalTable: "analysis_rules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_analysis_rule_product_products_products_id",
                        column: x => x.products_id,
                        principalSchema: "product",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_tool",
                schema: "product",
                columns: table => new
                {
                    products_id = table.Column<int>(type: "integer", nullable: false),
                    tools_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_tool", x => new { x.products_id, x.tools_id });
                    table.ForeignKey(
                        name: "fk_product_tool_products_products_id",
                        column: x => x.products_id,
                        principalSchema: "product",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_tool_tools_tools_id",
                        column: x => x.tools_id,
                        principalSchema: "product",
                        principalTable: "tools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_user_client_id",
                schema: "product",
                table: "product_user",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_user_product_id",
                schema: "product",
                table: "product_user",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_analysis_rule_product_products_id",
                table: "analysis_rule_product",
                column: "products_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_tool_tools_id",
                schema: "product",
                table: "product_tool",
                column: "tools_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_user_users_user_id",
                schema: "product",
                table: "product_user",
                column: "user_id",
                principalSchema: "auth",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "product_user_client_id_fkey",
                schema: "product",
                table: "product_user",
                column: "client_id",
                principalSchema: "product",
                principalTable: "clients",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "product_user_organization_id_fkey",
                schema: "product",
                table: "product_user",
                column: "organization_id",
                principalSchema: "product",
                principalTable: "organizations",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_user_users_user_id",
                schema: "product",
                table: "product_user");

            migrationBuilder.DropForeignKey(
                name: "product_user_client_id_fkey",
                schema: "product",
                table: "product_user");

            migrationBuilder.DropForeignKey(
                name: "product_user_organization_id_fkey",
                schema: "product",
                table: "product_user");

            migrationBuilder.DropTable(
                name: "analysis_rule_product");

            migrationBuilder.DropTable(
                name: "product_tool",
                schema: "product");

            migrationBuilder.DropPrimaryKey(
                name: "product_user_pkey",
                schema: "product",
                table: "product_user");

            migrationBuilder.DropIndex(
                name: "ix_product_user_client_id",
                schema: "product",
                table: "product_user");

            migrationBuilder.DropIndex(
                name: "ix_product_user_product_id",
                schema: "product",
                table: "product_user");

            migrationBuilder.DropColumn(
                name: "organization_id",
                schema: "product",
                table: "product_user");

            migrationBuilder.DropColumn(
                name: "client_id",
                schema: "product",
                table: "product_user");

            migrationBuilder.DropColumn(
                name: "interaction_type",
                schema: "rule",
                table: "analysis_rules");

            migrationBuilder.AddColumn<int>(
                name: "product_id",
                schema: "product",
                table: "tools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "product_id",
                schema: "rule",
                table: "analysis_rules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "product_user_pkey",
                schema: "product",
                table: "product_user",
                columns: new[] { "product_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "ix_tools_name_product_id",
                schema: "product",
                table: "tools",
                columns: new[] { "name", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tools_product_id",
                schema: "product",
                table: "tools",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_analysis_rules_product_id",
                schema: "rule",
                table: "analysis_rules",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "analysis_rules_product_id_fkey",
                schema: "rule",
                table: "analysis_rules",
                column: "product_id",
                principalSchema: "product",
                principalTable: "products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "product_user_user_id_fkey",
                schema: "product",
                table: "product_user",
                column: "user_id",
                principalSchema: "auth",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "tools_product_id_fkey",
                schema: "product",
                table: "tools",
                column: "product_id",
                principalSchema: "product",
                principalTable: "products",
                principalColumn: "id");
        }
    }
}

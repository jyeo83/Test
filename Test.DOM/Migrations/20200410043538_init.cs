using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.DOM.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    ContractorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.ContractorId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    District = table.Column<int>(nullable: false),
                    EA = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    TradeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.TradeId);
                });

            migrationBuilder.CreateTable(
                name: "AssistantDailyReports",
                columns: table => new
                {
                    AssistantDailyReportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Narrative = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistantDailyReports", x => x.AssistantDailyReportId);
                    table.ForeignKey(
                        name: "FK_AssistantDailyReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectContractors",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    ContractorId = table.Column<int>(nullable: false),
                    IsPrime = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectContractors", x => new { x.ProjectId, x.ContractorId });
                    table.ForeignKey(
                        name: "FK_ProjectContractors_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "ContractorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectContractors_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Labors",
                columns: table => new
                {
                    LaborId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractorId = table.Column<int>(nullable: false),
                    TradeId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labors", x => x.LaborId);
                    table.ForeignKey(
                        name: "FK_Labors_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "ContractorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Labors_Trade_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trade",
                        principalColumn: "TradeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkPerformed",
                columns: table => new
                {
                    WorkPerformedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssistantDailyReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPerformed", x => x.WorkPerformedId);
                    table.ForeignKey(
                        name: "FK_WorkPerformed_AssistantDailyReports_AssistantDailyReportId",
                        column: x => x.AssistantDailyReportId,
                        principalTable: "AssistantDailyReports",
                        principalColumn: "AssistantDailyReportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectLabors",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    LaborId = table.Column<int>(nullable: false),
                    TradeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLabors", x => new { x.ProjectId, x.LaborId, x.TradeId });
                    table.ForeignKey(
                        name: "FK_ProjectLabors_Labors_LaborId",
                        column: x => x.LaborId,
                        principalTable: "Labors",
                        principalColumn: "LaborId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectLabors_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectLabors_Trade_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trade",
                        principalColumn: "TradeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaborWorkPerformed",
                columns: table => new
                {
                    WorkPerformedId = table.Column<int>(nullable: false),
                    LaborId = table.Column<int>(nullable: false),
                    RegHours = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    OtHours = table.Column<decimal>(type: "decimal(3,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborWorkPerformed", x => new { x.WorkPerformedId, x.LaborId });
                    table.ForeignKey(
                        name: "FK_LaborWorkPerformed_Labors_LaborId",
                        column: x => x.LaborId,
                        principalTable: "Labors",
                        principalColumn: "LaborId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaborWorkPerformed_WorkPerformed_WorkPerformedId",
                        column: x => x.WorkPerformedId,
                        principalTable: "WorkPerformed",
                        principalColumn: "WorkPerformedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssistantDailyReports_ProjectId",
                table: "AssistantDailyReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Labors_ContractorId",
                table: "Labors",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Labors_TradeId",
                table: "Labors",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_LaborWorkPerformed_LaborId",
                table: "LaborWorkPerformed",
                column: "LaborId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectContractors_ContractorId",
                table: "ProjectContractors",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLabors_LaborId",
                table: "ProjectLabors",
                column: "LaborId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLabors_TradeId",
                table: "ProjectLabors",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPerformed_AssistantDailyReportId",
                table: "WorkPerformed",
                column: "AssistantDailyReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaborWorkPerformed");

            migrationBuilder.DropTable(
                name: "ProjectContractors");

            migrationBuilder.DropTable(
                name: "ProjectLabors");

            migrationBuilder.DropTable(
                name: "WorkPerformed");

            migrationBuilder.DropTable(
                name: "Labors");

            migrationBuilder.DropTable(
                name: "AssistantDailyReports");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}

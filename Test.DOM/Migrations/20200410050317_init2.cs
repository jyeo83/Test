using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.DOM.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLabors_Trade_TradeId",
                table: "ProjectLabors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectLabors",
                table: "ProjectLabors");

            migrationBuilder.DropIndex(
                name: "IX_ProjectLabors_TradeId",
                table: "ProjectLabors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LaborWorkPerformed",
                table: "LaborWorkPerformed");

            migrationBuilder.DropColumn(
                name: "TradeId",
                table: "ProjectLabors");

            migrationBuilder.AddColumn<int>(
                name: "TradeId",
                table: "LaborWorkPerformed",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectLabors",
                table: "ProjectLabors",
                columns: new[] { "ProjectId", "LaborId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LaborWorkPerformed",
                table: "LaborWorkPerformed",
                columns: new[] { "WorkPerformedId", "LaborId", "TradeId" });

            migrationBuilder.CreateIndex(
                name: "IX_LaborWorkPerformed_TradeId",
                table: "LaborWorkPerformed",
                column: "TradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LaborWorkPerformed_Trade_TradeId",
                table: "LaborWorkPerformed",
                column: "TradeId",
                principalTable: "Trade",
                principalColumn: "TradeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaborWorkPerformed_Trade_TradeId",
                table: "LaborWorkPerformed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectLabors",
                table: "ProjectLabors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LaborWorkPerformed",
                table: "LaborWorkPerformed");

            migrationBuilder.DropIndex(
                name: "IX_LaborWorkPerformed_TradeId",
                table: "LaborWorkPerformed");

            migrationBuilder.DropColumn(
                name: "TradeId",
                table: "LaborWorkPerformed");

            migrationBuilder.AddColumn<int>(
                name: "TradeId",
                table: "ProjectLabors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectLabors",
                table: "ProjectLabors",
                columns: new[] { "ProjectId", "LaborId", "TradeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LaborWorkPerformed",
                table: "LaborWorkPerformed",
                columns: new[] { "WorkPerformedId", "LaborId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLabors_TradeId",
                table: "ProjectLabors",
                column: "TradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLabors_Trade_TradeId",
                table: "ProjectLabors",
                column: "TradeId",
                principalTable: "Trade",
                principalColumn: "TradeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

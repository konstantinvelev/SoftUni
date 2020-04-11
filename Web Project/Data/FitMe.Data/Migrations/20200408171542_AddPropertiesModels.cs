using Microsoft.EntityFrameworkCore.Migrations;

namespace FitMe.Data.Migrations
{
    public partial class AddPropertiesModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Diets_DietsId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_DietsId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "DietsId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "TypeOfGender",
                table: "Diets");

            migrationBuilder.AddColumn<string>(
                name: "DietId",
                table: "Votes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "Votes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_DietId",
                table: "Votes",
                column: "DietId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Diets_DietId",
                table: "Votes",
                column: "DietId",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Diets_DietId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_DietId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "DietId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "DietsId",
                table: "Votes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfGender",
                table: "Diets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_DietsId",
                table: "Votes",
                column: "DietsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Diets_DietsId",
                table: "Votes",
                column: "DietsId",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

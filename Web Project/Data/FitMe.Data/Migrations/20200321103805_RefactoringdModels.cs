using Microsoft.EntityFrameworkCore.Migrations;

namespace FitMe.Data.Migrations
{
    public partial class RefactoringdModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeOfGender",
                table: "Exercises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfGender",
                table: "Diets",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfGender",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "TypeOfGender",
                table: "Diets");
        }
    }
}

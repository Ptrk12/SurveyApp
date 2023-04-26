using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_property_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SurveyQuestionAnswerEntity",
                keyColumn: "Id",
                keyValue: 4,
                column: "Answer",
                value: "Park Forest");

            migrationBuilder.UpdateData(
                table: "SurveyQuestionAnswerEntity",
                keyColumn: "Id",
                keyValue: 5,
                column: "Answer",
                value: "Cycling Running");

            migrationBuilder.UpdateData(
                table: "SurveyQuestionAnswerEntity",
                keyColumn: "Id",
                keyValue: 6,
                column: "Answer",
                value: "Pizza Burger");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SurveyQuestionAnswerEntity",
                keyColumn: "Id",
                keyValue: 4,
                column: "Answer",
                value: "Park,Forest");

            migrationBuilder.UpdateData(
                table: "SurveyQuestionAnswerEntity",
                keyColumn: "Id",
                keyValue: 5,
                column: "Answer",
                value: "Cycling,Running");

            migrationBuilder.UpdateData(
                table: "SurveyQuestionAnswerEntity",
                keyColumn: "Id",
                keyValue: 6,
                column: "Answer",
                value: "Pizza,Burger");
        }
    }
}

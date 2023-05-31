using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedNewFieldMAxAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfMaxAnswers",
                table: "SurveyQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "657d2be8-a49a-421a-8eb0-9240192b64b1", "AQAAAAIAAYagAAAAEBwAdFk6p4w2G2pMrz5rWYyy+dFTiE67ijOXg+eMC8r9kDqSBT4Q4OJXehtVPSM2kQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f08ff384-5033-403d-ad37-4496719b58eb", "AQAAAAIAAYagAAAAELmfmDFB8MzmQfnR9S0LXs3YBL5RI3KTkxzG7yhFYJk+83V8PcSUElmXVVSwqtD2Og==" });

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 1,
                column: "NumberOfMaxAnswers",
                value: null);

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 2,
                column: "NumberOfMaxAnswers",
                value: null);

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 3,
                column: "NumberOfMaxAnswers",
                value: null);

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 4,
                column: "NumberOfMaxAnswers",
                value: null);

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 5,
                column: "NumberOfMaxAnswers",
                value: null);

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 6,
                column: "NumberOfMaxAnswers",
                value: null);

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 7,
                column: "NumberOfMaxAnswers",
                value: null);

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 8,
                column: "NumberOfMaxAnswers",
                value: null);

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 9,
                column: "NumberOfMaxAnswers",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfMaxAnswers",
                table: "SurveyQuestions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "15d2a6ed-2b49-4f9b-9e10-58d9053c23f7", "AQAAAAIAAYagAAAAEMksacbudj4cD84QtvdKG9DxPoHLNdSclH5hHbKByQW4Zjq4pZVcb85dEAXWd1cJhw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e803adb2-3899-472c-8f84-06a21a875e5b", "AQAAAAIAAYagAAAAEDnfNuaL9hA0XDUSKCmTXFJYGnIaTBGImc1eK1t8uO+b5sBnWluOIoyKNGdQC39BYw==" });
        }
    }
}

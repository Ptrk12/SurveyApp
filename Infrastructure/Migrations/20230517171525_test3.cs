using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d8a51138-6041-49a4-99b5-36e7975ed891", "AQAAAAIAAYagAAAAEHXDCOIiG7XL7YY3WSAcJ0sL3ML3xgSSXv9eb0KslhhfKOhyKLhMFLVfhRX+mswZxQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4ef1d9a8-dc58-4683-8ee5-9beda74722fa", "AQAAAAIAAYagAAAAEOjJEZOQaDXC83PWWhKhi8kFfxvqbdgUCJiMj7GEK+bfEToZVAl7mqn27FpQ/H0kDw==" });
        }
    }
}

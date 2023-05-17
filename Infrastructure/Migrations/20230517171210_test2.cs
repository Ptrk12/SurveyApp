using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_AspNetUsers_UserId",
                table: "Surveys");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Surveys",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_AspNetUsers_UserId",
                table: "Surveys",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_AspNetUsers_UserId",
                table: "Surveys");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Surveys",
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
                values: new object[] { "fbd69297-3ee7-4903-9f9e-d8e5e16c70c9", "AQAAAAIAAYagAAAAEDhDfMJ/8vfaANoWLRJYwHuz35fA4CwusS8DWh/VWu/Mz+3buiwIwfOyjImHDIOr8Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6d4615a0-06f7-4e95-819a-3758989c223f", "AQAAAAIAAYagAAAAEHfrJtDU6/4EhT2medsQgtcNUlSKMvYkrXe5cVSvzB8trnq+C+9y9mSdwqE70d6g+A==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_AspNetUsers_UserId",
                table: "Surveys",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "33ab19a1-a4df-4ddb-a088-a2a276f9c9ec", "NORMALUSER", "AQAAAAIAAYagAAAAEKGUyen+V4IeQG8wD8S2D35jPvhhPwGi0v2fi1KTAp6yRw3QYzpuBlFh1eaOVWQEHA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ded9a775-8b7a-4775-8254-4128edbec0e2", "AQAAAAIAAYagAAAAECb3lDya2pilkIoCChcFvX+JR/Iuln6o4w7eLUZu82Bk7NXrNIevzQmqGL8rJ+LwyQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "05824ba0-d8de-4754-8909-72d9c0a42c2b", "MYUSER", "AQAAAAIAAYagAAAAEBGigjFkzjFnA/587pADOyP4hVdvuxZAfbHypztvCYBmN4cjI2frhnj+IOeWh3fzVQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f90c8937-40d0-4c91-a8ed-a6dad57a7b6e", "AQAAAAIAAYagAAAAEKhda+OEptgJnpQAN4yOLS1O2rSID61FeaNYLeBTczd9dPjduX9KoHLjyj3FpeXQcA==" });
        }
    }
}

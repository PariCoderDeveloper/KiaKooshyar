using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KiaKooshar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CleanupHasDataAndFixCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: -6L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: -5L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: -4L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: -3L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: -2L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: -1L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Code", "CreatedAt", "DiplayName", "IsDeleted", "UpdatedAt" },
                values: new object[,]
                {
                    { -6L, "User.Block", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User Block", false, null },
                    { -5L, "User.Disable", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Disable User", false, null },
                    { -4L, "User.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Delete User", false, null },
                    { -3L, "User.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Update User", false, null },
                    { -2L, "User.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create User", false, null },
                    { -1L, "User.View", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "View Users", false, null }
                });
        }
    }
}

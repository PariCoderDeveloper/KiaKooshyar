using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KiaKooshar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRolePermissionSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "PermissionId", "RoleId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, -1L, 1L, null },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, -2L, 1L, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}

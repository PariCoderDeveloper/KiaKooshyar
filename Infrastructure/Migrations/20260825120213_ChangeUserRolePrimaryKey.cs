using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiaKooshar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserRolePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up ( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.CreateTable (
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<long> (type: "bigint", nullable: false)
                        .Annotation ("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long> (type: "bigint", nullable: false),
                    RoleId = table.Column<long> (type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime> (type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime> (type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool> (type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]> (type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey ("PK_UserRoles", x => x.Id);
                    table.ForeignKey (
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey (
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex (
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex (
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down ( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.DropTable (
                name: "UserRoles");
        }
    }
}
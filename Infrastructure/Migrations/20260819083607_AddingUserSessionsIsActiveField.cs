using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiaKooshar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserSessionsIsActiveField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserSessions");
        }
    }
}

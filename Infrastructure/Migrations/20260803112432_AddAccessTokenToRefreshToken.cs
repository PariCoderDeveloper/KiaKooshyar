using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiaKooshar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAccessTokenToRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "RefreshTokens");
        }
    }
}

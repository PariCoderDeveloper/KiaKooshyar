using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiaKooshar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFileSizeField : Migration
    {
        /// <inheritdoc />
        protected override void Up ( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.AddColumn<long> (
                name: "FileSize",
                table: "UploadedFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down ( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.DropColumn (
                name: "FileSize",
                table: "UploadedFiles");
        }
    }
}

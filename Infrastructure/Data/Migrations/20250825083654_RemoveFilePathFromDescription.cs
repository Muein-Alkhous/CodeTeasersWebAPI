using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFilePathFromDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description_path",
                table: "descriptions");

            migrationBuilder.AddColumn<byte[]>(
                name: "file_data",
                table: "descriptions",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_data",
                table: "descriptions");

            migrationBuilder.AddColumn<string>(
                name: "description_path",
                table: "descriptions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreativeToolsAggregatorApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tag",
                table: "Tools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tag",
                table: "Tools");
        }
    }
}

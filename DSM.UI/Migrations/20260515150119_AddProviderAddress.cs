using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSM.UI.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Services");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSM.UI.Migrations
{
    /// <inheritdoc />
    public partial class FinalBookingUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BookingRequests",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProviderName",
                table: "BookingRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "ProviderName",
                table: "BookingRequests");
        }
    }
}

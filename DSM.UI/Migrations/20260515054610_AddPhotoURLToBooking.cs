using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSM.UI.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoURLToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoURL",
                table: "BookingRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoURL",
                table: "BookingRequests");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyShop1.Migrations
{
    /// <inheritdoc />
    public partial class teste21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CheckoutLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CheckoutLogs");
        }
    }
}

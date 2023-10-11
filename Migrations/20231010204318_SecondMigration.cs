using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingPlanner.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuestTwo",
                table: "Weddings",
                newName: "WedderTwo");

            migrationBuilder.RenameColumn(
                name: "GuestOne",
                table: "Weddings",
                newName: "WedderOne");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WedderTwo",
                table: "Weddings",
                newName: "GuestTwo");

            migrationBuilder.RenameColumn(
                name: "WedderOne",
                table: "Weddings",
                newName: "GuestOne");
        }
    }
}

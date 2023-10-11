using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingPlanner.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decisions_Weddings_WeddingId",
                table: "Decisions");

            migrationBuilder.DropColumn(
                name: "WeddinId",
                table: "Decisions");

            migrationBuilder.AlterColumn<int>(
                name: "WeddingId",
                table: "Decisions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Decisions_Weddings_WeddingId",
                table: "Decisions",
                column: "WeddingId",
                principalTable: "Weddings",
                principalColumn: "WeddingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decisions_Weddings_WeddingId",
                table: "Decisions");

            migrationBuilder.AlterColumn<int>(
                name: "WeddingId",
                table: "Decisions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WeddinId",
                table: "Decisions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Decisions_Weddings_WeddingId",
                table: "Decisions",
                column: "WeddingId",
                principalTable: "Weddings",
                principalColumn: "WeddingId");
        }
    }
}

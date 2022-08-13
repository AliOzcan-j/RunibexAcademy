using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class SplitNameProperty_ForModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Models",
                newName: "NamePrefix");

            migrationBuilder.AddColumn<string>(
                name: "NameSuffix",
                table: "Models",
                type: "varchar(50)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameSuffix",
                table: "Models");

            migrationBuilder.RenameColumn(
                name: "NamePrefix",
                table: "Models",
                newName: "Name");
        }
    }
}

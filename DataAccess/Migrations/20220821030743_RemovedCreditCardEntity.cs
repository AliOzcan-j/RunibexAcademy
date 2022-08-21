using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class RemovedCreditCardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_CreditCards_CreditCardId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CreditCardId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreditCardId",
                table: "Payments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditCardId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CardHolderNameHash = table.Column<byte[]>(type: "varbinary(500)", nullable: false),
                    CardHolderNameSalt = table.Column<byte[]>(type: "varbinary(500)", nullable: false),
                    CardNumberHash = table.Column<byte[]>(type: "varbinary(500)", nullable: false),
                    CardNumberSalt = table.Column<byte[]>(type: "varbinary(500)", nullable: false),
                    ExpirationDateHash = table.Column<byte[]>(type: "varbinary(500)", nullable: false),
                    ExpirationDateSalt = table.Column<byte[]>(type: "varbinary(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CreditCardId",
                table: "Payments",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_UserId",
                table: "CreditCards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_CreditCards_CreditCardId",
                table: "Payments",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

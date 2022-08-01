using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class PropertyChange_ForCar_ForCreditCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Milage",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "CreditCards",
                newName: "ExpirationDateSalt");

            migrationBuilder.RenameColumn(
                name: "CardNumber",
                table: "CreditCards",
                newName: "ExpirationDateHash");

            migrationBuilder.RenameColumn(
                name: "CardHolderName",
                table: "CreditCards",
                newName: "CardNumberSalt");

            migrationBuilder.AddColumn<byte[]>(
                name: "CardHolderNameHash",
                table: "CreditCards",
                type: "varbinary(500)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "CardHolderNameSalt",
                table: "CreditCards",
                type: "varbinary(500)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "CardNumberHash",
                table: "CreditCards",
                type: "varbinary(500)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MilageLimit",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardHolderNameHash",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "CardHolderNameSalt",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "CardNumberHash",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "MilageLimit",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "ExpirationDateSalt",
                table: "CreditCards",
                newName: "ExpirationDate");

            migrationBuilder.RenameColumn(
                name: "ExpirationDateHash",
                table: "CreditCards",
                newName: "CardNumber");

            migrationBuilder.RenameColumn(
                name: "CardNumberSalt",
                table: "CreditCards",
                newName: "CardHolderName");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Cars",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Milage",
                table: "Cars",
                type: "varchar(15)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

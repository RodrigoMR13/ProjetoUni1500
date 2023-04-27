using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemFH.Migrations
{
    public partial class ALterBankAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CashMonth",
                table: "CashManager",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "CashManager",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ActualBalance",
                table: "BankAccounts",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "EnterpriseId",
                table: "BankAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "InitialBalance",
                table: "BankAccounts",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_EnterpriseId",
                table: "BankAccounts",
                column: "EnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Enterprise_EnterpriseId",
                table: "BankAccounts",
                column: "EnterpriseId",
                principalTable: "Enterprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Enterprise_EnterpriseId",
                table: "BankAccounts");

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_EnterpriseId",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "CashManager");

            migrationBuilder.DropColumn(
                name: "ActualBalance",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "InitialBalance",
                table: "BankAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "CashMonth",
                table: "CashManager",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}

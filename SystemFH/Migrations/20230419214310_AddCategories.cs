using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemFH.Migrations
{
    public partial class AddCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashManager_BankAccount_BankAccountId",
                table: "CashManager");

            migrationBuilder.DropForeignKey(
                name: "FK_CashManager_ClientProject_CategoryId",
                table: "CashManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientProject",
                table: "ClientProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount");

            migrationBuilder.RenameTable(
                name: "ClientProject",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "BankAccount",
                newName: "BankAccounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashManager_BankAccounts_BankAccountId",
                table: "CashManager",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashManager_Category_CategoryId",
                table: "CashManager",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashManager_BankAccounts_BankAccountId",
                table: "CashManager");

            migrationBuilder.DropForeignKey(
                name: "FK_CashManager_Category_CategoryId",
                table: "CashManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "ClientProject");

            migrationBuilder.RenameTable(
                name: "BankAccounts",
                newName: "BankAccount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientProject",
                table: "ClientProject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashManager_BankAccount_BankAccountId",
                table: "CashManager",
                column: "BankAccountId",
                principalTable: "BankAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashManager_ClientProject_CategoryId",
                table: "CashManager",
                column: "CategoryId",
                principalTable: "ClientProject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

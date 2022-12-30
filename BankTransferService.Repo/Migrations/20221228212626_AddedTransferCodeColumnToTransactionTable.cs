using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransferService.Repo.Migrations
{
    public partial class AddedTransferCodeColumnToTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransferCode",
                table: "TransactionHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransferCode",
                table: "TransactionHistories");
        }
    }
}

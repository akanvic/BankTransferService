using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransferService.Repo.Migrations
{
    public partial class AddedMaxAttemptColumnToTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxRetryAttempt",
                table: "TransactionHistories",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxRetryAttempt",
                table: "TransactionHistories");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class receiverCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiverCategoryId",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ReceiverCategoryId",
                table: "Transaction",
                column: "ReceiverCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Category_ReceiverCategoryId",
                table: "Transaction",
                column: "ReceiverCategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Category_ReceiverCategoryId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_ReceiverCategoryId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ReceiverCategoryId",
                table: "Transaction");
        }
    }
}

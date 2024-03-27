using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infratructure.Migrations
{
    public partial class somechangeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Orders_OrderId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Ships_OrderId",
                table: "Ships");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ships_OrderId",
                table: "Ships",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Orders_OrderId",
                table: "Ships",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Orders_OrderId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Ships_OrderId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_OrderId",
                table: "Ships",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Orders_OrderId",
                table: "Ships",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

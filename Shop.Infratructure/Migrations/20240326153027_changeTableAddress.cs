using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infratructure.Migrations
{
    public partial class changeTableAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Addresses_AddressId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Ships_AddressId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Ships");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Ships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Commune",
                table: "Ships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Ships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Ships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Commune",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Ships");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Ships",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Ships_AddressId",
                table: "Ships",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Addresses_AddressId",
                table: "Ships",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}

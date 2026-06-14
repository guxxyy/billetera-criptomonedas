using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilleteraCriptomonedasBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Clientes_ClienteId",
                table: "Transacciones");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_ClienteId",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "CantidadComprada",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "Hora",
                table: "Transacciones");

            migrationBuilder.RenameColumn(
                name: "Criptomoneda",
                table: "Transacciones",
                newName: "CryptoCode");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Transacciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Transacciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "CryptoAmount",
                table: "Transacciones",
                type: "decimal(18,8)",
                precision: 18,
                scale: 8,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MoneySpent",
                table: "Transacciones",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDateTime",
                table: "Transacciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "CryptoAmount",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "MoneySpent",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "TransactionDateTime",
                table: "Transacciones");

            migrationBuilder.RenameColumn(
                name: "CryptoCode",
                table: "Transacciones",
                newName: "Criptomoneda");

            migrationBuilder.AddColumn<decimal>(
                name: "CantidadComprada",
                table: "Transacciones",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Fecha",
                table: "Transacciones",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Hora",
                table: "Transacciones",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_ClienteId",
                table: "Transacciones",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Clientes_ClienteId",
                table: "Transacciones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

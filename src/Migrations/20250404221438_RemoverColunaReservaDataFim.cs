using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTBitzen.Migrations
{
    /// <inheritdoc />
    public partial class RemoverColunaReservaDataFim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservas_DataFim_HoraFim",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "Reservas",
                newName: "Data");

            migrationBuilder.RenameIndex(
                name: "IX_Reservas_DataInicio_HoraInicio",
                table: "Reservas",
                newName: "IX_Reservas_Data_HoraInicio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Reservas",
                newName: "DataInicio");

            migrationBuilder.RenameIndex(
                name: "IX_Reservas_Data_HoraInicio",
                table: "Reservas",
                newName: "IX_Reservas_DataInicio_HoraInicio");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataFim",
                table: "Reservas",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_DataFim_HoraFim",
                table: "Reservas",
                columns: new[] { "DataFim", "HoraFim" },
                unique: true);
        }
    }
}

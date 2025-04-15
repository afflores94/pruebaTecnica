using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnica_ArielFlores.Migrations
{
    /// <inheritdoc />
    public partial class afflores9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_cuentas_NumeroCuenta",
                table: "cuentas",
                column: "NumeroCuenta",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cuentas_NumeroCuenta",
                table: "cuentas");
        }
    }
}

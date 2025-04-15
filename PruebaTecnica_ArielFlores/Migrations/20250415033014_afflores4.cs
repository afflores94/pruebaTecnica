using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnica_ArielFlores.Migrations
{
    /// <inheritdoc />
    public partial class afflores4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cuentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroCuenta = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    ClientesId = table.Column<int>(type: "INTEGER", nullable: false),
                    Saldo = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cuentas_clientes_ClientesId",
                        column: x => x.ClientesId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cuentas_ClientesId",
                table: "cuentas",
                column: "ClientesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cuentas");
        }
    }
}

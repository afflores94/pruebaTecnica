using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnica_ArielFlores.Migrations
{
    /// <inheritdoc />
    public partial class afflores1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identificacion",
                table: "clientes",
                type: "TEXT",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identificacion",
                table: "clientes");
        }
    }
}

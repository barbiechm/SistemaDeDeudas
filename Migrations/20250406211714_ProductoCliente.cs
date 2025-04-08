using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeDeudas.Migrations
{
    /// <inheritdoc />
    public partial class ProductoCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Producto",
                table: "Clientes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Producto",
                table: "Clientes");
        }
    }
}

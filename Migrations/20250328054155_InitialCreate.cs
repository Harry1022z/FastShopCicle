using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaCiclismo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Productos_ProductoId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_ProveedorId1",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_ProveedorId1",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "ProveedorId1",
                table: "Productos");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Productos_ProductoId",
                table: "Facturas",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Productos_ProductoId",
                table: "Facturas");

            migrationBuilder.AddColumn<int>(
                name: "ProveedorId1",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProveedorId1",
                table: "Productos",
                column: "ProveedorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Productos_ProductoId",
                table: "Facturas",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_ProveedorId1",
                table: "Productos",
                column: "ProveedorId1",
                principalTable: "Proveedores",
                principalColumn: "Id");
        }
    }
}

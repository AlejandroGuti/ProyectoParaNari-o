using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto.Migrations
{
    public partial class Creacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true),
                    Detalle = table.Column<string>(nullable: true),
                    Descuento = table.Column<decimal>(nullable: false),
                    Iva = table.Column<decimal>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Referencia = table.Column<string>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false),
                    FotoPrincipal = table.Column<string>(nullable: true),
                    TiempoGarantia = table.Column<string>(nullable: true),
                    Garantia = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}

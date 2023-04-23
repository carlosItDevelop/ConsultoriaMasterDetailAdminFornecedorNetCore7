using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooperchip.ITDeveloper.Farmacia.InfraData.Migrations
{
    public partial class AddRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RepresentanteLegal_FornecedorId",
                table: "RepresentanteLegal");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentanteLegal_FornecedorId",
                table: "RepresentanteLegal",
                column: "FornecedorId",
                unique: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RepresentanteLegal_FornecedorId",
                table: "RepresentanteLegal");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentanteLegal_FornecedorId",
                table: "RepresentanteLegal",
                column: "FornecedorId");

        }
    }
}

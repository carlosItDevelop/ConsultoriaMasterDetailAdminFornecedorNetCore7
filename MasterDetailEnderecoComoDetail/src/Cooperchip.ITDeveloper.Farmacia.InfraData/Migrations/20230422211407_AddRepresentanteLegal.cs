using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooperchip.ITDeveloper.Farmacia.InfraData.Migrations
{
    public partial class AddRepresentanteLegal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepresentanteLegal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    SobreNome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Documento = table.Column<string>(type: "varchar(14)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true),
                    Telefone = table.Column<string>(type: "varchar(13)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentanteLegal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepresentanteLegal_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepresentanteLegal_FornecedorId",
                table: "RepresentanteLegal",
                column: "FornecedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepresentanteLegal");
        }
    }
}

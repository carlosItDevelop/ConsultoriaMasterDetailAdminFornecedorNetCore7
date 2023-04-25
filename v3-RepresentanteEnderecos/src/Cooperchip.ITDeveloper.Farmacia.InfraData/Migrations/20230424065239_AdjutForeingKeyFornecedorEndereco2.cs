using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooperchip.ITDeveloper.Farmacia.InfraData.Migrations
{
    public partial class AdjutForeingKeyFornecedorEndereco2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProdutoId",
                table: "Fornecedores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_ProdutoId",
                table: "Fornecedores",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedores_Produtos_ProdutoId",
                table: "Fornecedores",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedores_Produtos_ProdutoId",
                table: "Fornecedores");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedores_ProdutoId",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Fornecedores");
        }
    }
}

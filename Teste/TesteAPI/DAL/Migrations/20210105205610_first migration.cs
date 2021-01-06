using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCliente = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Arquivo",
                columns: table => new
                {
                    IdArquivo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(nullable: false),
                    NomeArquivo = table.Column<string>(nullable: false),
                    DataEnvio = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivo", x => x.IdArquivo);
                    table.ForeignKey(
                        name: "FK_Arquivo_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    IdDespesa = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdArquivo = table.Column<int>(nullable: false),
                    TipoDespesa = table.Column<int>(nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: true),
                    DataPagamento = table.Column<DateTime>(nullable: true),
                    ValorCobrado = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorMulta = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesa", x => x.IdDespesa);
                    table.ForeignKey(
                        name: "FK_Despesa_Arquivo_IdArquivo",
                        column: x => x.IdArquivo,
                        principalTable: "Arquivo",
                        principalColumn: "IdArquivo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "IdCliente", "NomeCliente" },
                values: new object[] { 1, "Auto Escola ABC (TESTE)" });

            migrationBuilder.CreateIndex(
                name: "IX_Arquivo_IdCliente",
                table: "Arquivo",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_IdArquivo",
                table: "Despesa",
                column: "IdArquivo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesa");

            migrationBuilder.DropTable(
                name: "Arquivo");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}

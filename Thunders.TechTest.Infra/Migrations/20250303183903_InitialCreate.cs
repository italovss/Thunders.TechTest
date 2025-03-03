using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thunders.TechTest.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedagioUtilizacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Praca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoVeiculo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedagioUtilizacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatoriosFaturamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPorHora = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatoriosFaturamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatoriosTopPracas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MesAno = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TopPracas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatoriosTopPracas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatoriosVeiculosPorPraca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Praca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantidadeVeiculos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatoriosVeiculosPorPraca", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedagioUtilizacoes");

            migrationBuilder.DropTable(
                name: "RelatoriosFaturamento");

            migrationBuilder.DropTable(
                name: "RelatoriosTopPracas");

            migrationBuilder.DropTable(
                name: "RelatoriosVeiculosPorPraca");
        }
    }
}

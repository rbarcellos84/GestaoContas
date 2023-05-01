using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoContas.InfraEstrutura.Migrations
{
    /// <inheritdoc />
    public partial class Pesquisa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PESQUISA",
                columns: table => new
                {
                    IDPESQUISA = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DATAINICIO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DATAFIM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CONTA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FORMATO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TIPOCONSULTA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PESQUISA", x => x.IDPESQUISA);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PESQUISA");
        }
    }
}

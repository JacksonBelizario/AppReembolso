using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "solicitacao",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    data_da_compra = table.Column<DateTime>(nullable: false),
                    data_da_solicitacao = table.Column<DateTime>(nullable: false),
                    valor = table.Column<double>(nullable: false),
                    categoria = table.Column<int>(nullable: false),
                    descricao = table.Column<string>(nullable: true),
                    latitude = table.Column<int>(nullable: false),
                    longitude = table.Column<int>(nullable: false),
                    anexo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solicitacao", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "solicitacao");
        }
    }
}

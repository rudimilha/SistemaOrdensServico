using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaOrdensServico.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarEquipamentosEAtualizarOrdens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServico_Clientes_ClienteId",
                table: "OrdensServico");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServico_Servicos_ServicoId",
                table: "OrdensServico");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServico_Tecnicos_TecnicoId",
                table: "OrdensServico");

            migrationBuilder.RenameColumn(
                name: "ServicoId",
                table: "OrdensServico",
                newName: "EquipamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdensServico_ServicoId",
                table: "OrdensServico",
                newName: "IX_OrdensServico_EquipamentoId");

            migrationBuilder.AddColumn<string>(
                name: "Diagnostico",
                table: "OrdensServico",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Solucao",
                table: "OrdensServico",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Equipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroSerie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipamentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_ClienteId",
                table: "Equipamentos",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServico_Clientes_ClienteId",
                table: "OrdensServico",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServico_Equipamentos_EquipamentoId",
                table: "OrdensServico",
                column: "EquipamentoId",
                principalTable: "Equipamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServico_Tecnicos_TecnicoId",
                table: "OrdensServico",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServico_Clientes_ClienteId",
                table: "OrdensServico");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServico_Equipamentos_EquipamentoId",
                table: "OrdensServico");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServico_Tecnicos_TecnicoId",
                table: "OrdensServico");

            migrationBuilder.DropTable(
                name: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "Diagnostico",
                table: "OrdensServico");

            migrationBuilder.DropColumn(
                name: "Solucao",
                table: "OrdensServico");

            migrationBuilder.RenameColumn(
                name: "EquipamentoId",
                table: "OrdensServico",
                newName: "ServicoId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdensServico_EquipamentoId",
                table: "OrdensServico",
                newName: "IX_OrdensServico_ServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServico_Clientes_ClienteId",
                table: "OrdensServico",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServico_Servicos_ServicoId",
                table: "OrdensServico",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServico_Tecnicos_TecnicoId",
                table: "OrdensServico",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

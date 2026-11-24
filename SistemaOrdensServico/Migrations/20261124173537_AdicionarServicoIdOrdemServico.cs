using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaOrdensServico.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarServicoIdOrdemServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServicoId",
                table: "OrdensServico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrdensServico_ServicoId",
                table: "OrdensServico",
                column: "ServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServico_Servicos_ServicoId",
                table: "OrdensServico",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServico_Servicos_ServicoId",
                table: "OrdensServico");

            migrationBuilder.DropIndex(
                name: "IX_OrdensServico_ServicoId",
                table: "OrdensServico");

            migrationBuilder.DropColumn(
                name: "ServicoId",
                table: "OrdensServico");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalColaborador.Migrations
{
    public partial class correcaoDeBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user = table.Column<string>(type: "TEXT", nullable: false),
                    senha = table.Column<string>(type: "TEXT", nullable: false),
                    FuncionarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_usuarios_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_FuncionarioId",
                table: "usuarios",
                column: "FuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}

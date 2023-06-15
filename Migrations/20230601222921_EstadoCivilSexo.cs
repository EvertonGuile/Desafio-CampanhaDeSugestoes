using Microsoft.EntityFrameworkCore.Migrations;

namespace aula_5.Migrations
{
    public partial class EstadoCivilSexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoCivilId",
                table: "Aluno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SexoId",
                table: "Aluno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EstadoCivil",
                columns: table => new
                {
                    EstadoCivilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoCivil", x => x.EstadoCivilId);
                });

            migrationBuilder.CreateTable(
                name: "Sexo",
                columns: table => new
                {
                    SexoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexo", x => x.SexoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_EstadoCivilId",
                table: "Aluno",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_SexoId",
                table: "Aluno",
                column: "SexoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_EstadoCivil_EstadoCivilId",
                table: "Aluno",
                column: "EstadoCivilId",
                principalTable: "EstadoCivil",
                principalColumn: "EstadoCivilId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Sexo_SexoId",
                table: "Aluno",
                column: "SexoId",
                principalTable: "Sexo",
                principalColumn: "SexoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_EstadoCivil_EstadoCivilId",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Sexo_SexoId",
                table: "Aluno");

            migrationBuilder.DropTable(
                name: "EstadoCivil");

            migrationBuilder.DropTable(
                name: "Sexo");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_EstadoCivilId",
                table: "Aluno");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_SexoId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "EstadoCivilId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "SexoId",
                table: "Aluno");
        }
    }
}

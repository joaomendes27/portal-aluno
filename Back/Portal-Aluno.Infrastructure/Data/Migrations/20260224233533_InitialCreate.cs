using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Portal_Aluno.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alunos",
                columns: table => new
                {
                    ra = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    cpf = table.Column<string>(type: "text", nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cep = table.Column<string>(type: "text", nullable: false),
                    rua = table.Column<string>(type: "text", nullable: false),
                    numero = table.Column<string>(type: "text", nullable: false),
                    bairro = table.Column<string>(type: "text", nullable: false),
                    cidade = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false),
                    pais = table.Column<string>(type: "text", nullable: false),
                    foto = table.Column<string>(type: "text", nullable: true),
                    celular = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_alunos", x => x.ra);
                });

            migrationBuilder.CreateTable(
                name: "cursos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: true),
                    grau = table.Column<string>(type: "text", nullable: true),
                    carga_horaria = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cursos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "disciplinas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: true),
                    carga_horaria = table.Column<int>(type: "integer", nullable: true),
                    limite_faltas = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_disciplinas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "password_reset_tokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    expiracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_password_reset_tokens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "professores",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    cpf = table.Column<string>(type: "text", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    foto = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_professores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "salas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    andar = table.Column<int>(type: "integer", nullable: true),
                    numero = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_salas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    tipo = table.Column<string>(type: "text", nullable: false),
                    referencia_id = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "matriculas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ra = table.Column<int>(type: "integer", nullable: false),
                    curso_id = table.Column<int>(type: "integer", nullable: false),
                    semestre = table.Column<int>(type: "integer", nullable: true),
                    turno = table.Column<string>(type: "text", nullable: true),
                    data_matricula = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: true),
                    forma_ingresso = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_matriculas", x => x.id);
                    table.ForeignKey(
                        name: "fk_matriculas_alunos_ra",
                        column: x => x.ra,
                        principalTable: "alunos",
                        principalColumn: "ra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_matriculas_cursos_curso_id",
                        column: x => x.curso_id,
                        principalTable: "cursos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "curso_disciplina",
                columns: table => new
                {
                    curso_id = table.Column<int>(type: "integer", nullable: false),
                    disciplina_id = table.Column<int>(type: "integer", nullable: false),
                    semestre = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_curso_disciplina", x => new { x.curso_id, x.disciplina_id });
                    table.ForeignKey(
                        name: "fk_curso_disciplina_cursos_curso_id",
                        column: x => x.curso_id,
                        principalTable: "cursos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_curso_disciplina_disciplinas_disciplina_id",
                        column: x => x.disciplina_id,
                        principalTable: "disciplinas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "turmas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    curso_id = table.Column<int>(type: "integer", nullable: false),
                    disciplina_id = table.Column<int>(type: "integer", nullable: false),
                    semestre = table.Column<int>(type: "integer", nullable: true),
                    ano = table.Column<int>(type: "integer", nullable: true),
                    turno = table.Column<string>(type: "text", nullable: true),
                    professor_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "text", nullable: true),
                    capacidade = table.Column<int>(type: "integer", nullable: true),
                    dia_semana = table.Column<string>(type: "text", nullable: true),
                    hora_aula_inicio = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    hora_aula_fim = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    sala_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_turmas", x => x.id);
                    table.ForeignKey(
                        name: "fk_turmas_cursos_curso_id",
                        column: x => x.curso_id,
                        principalTable: "cursos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_turmas_disciplinas_disciplina_id",
                        column: x => x.disciplina_id,
                        principalTable: "disciplinas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_turmas_professores_professor_id",
                        column: x => x.professor_id,
                        principalTable: "professores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_turmas_salas_sala_id",
                        column: x => x.sala_id,
                        principalTable: "salas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "matricula_turma",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    matricula_id = table.Column<int>(type: "integer", nullable: false),
                    turma_id = table.Column<int>(type: "integer", nullable: false),
                    nota = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true),
                    faltas = table.Column<int>(type: "integer", nullable: true),
                    situacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_matricula_turma", x => x.id);
                    table.ForeignKey(
                        name: "fk_matricula_turma_matriculas_matricula_id",
                        column: x => x.matricula_id,
                        principalTable: "matriculas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_matricula_turma_turmas_turma_id",
                        column: x => x.turma_id,
                        principalTable: "turmas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_curso_disciplina_disciplina_id",
                table: "curso_disciplina",
                column: "disciplina_id");

            migrationBuilder.CreateIndex(
                name: "ix_matricula_turma_matricula_id",
                table: "matricula_turma",
                column: "matricula_id");

            migrationBuilder.CreateIndex(
                name: "ix_matricula_turma_turma_id",
                table: "matricula_turma",
                column: "turma_id");

            migrationBuilder.CreateIndex(
                name: "ix_matriculas_curso_id",
                table: "matriculas",
                column: "curso_id");

            migrationBuilder.CreateIndex(
                name: "ix_matriculas_ra",
                table: "matriculas",
                column: "ra",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_turmas_curso_id",
                table: "turmas",
                column: "curso_id");

            migrationBuilder.CreateIndex(
                name: "ix_turmas_disciplina_id",
                table: "turmas",
                column: "disciplina_id");

            migrationBuilder.CreateIndex(
                name: "ix_turmas_professor_id",
                table: "turmas",
                column: "professor_id");

            migrationBuilder.CreateIndex(
                name: "ix_turmas_sala_id",
                table: "turmas",
                column: "sala_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_login",
                table: "usuarios",
                column: "login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "curso_disciplina");

            migrationBuilder.DropTable(
                name: "matricula_turma");

            migrationBuilder.DropTable(
                name: "password_reset_tokens");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "matriculas");

            migrationBuilder.DropTable(
                name: "turmas");

            migrationBuilder.DropTable(
                name: "alunos");

            migrationBuilder.DropTable(
                name: "cursos");

            migrationBuilder.DropTable(
                name: "disciplinas");

            migrationBuilder.DropTable(
                name: "professores");

            migrationBuilder.DropTable(
                name: "salas");
        }
    }
}

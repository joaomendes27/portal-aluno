using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;
using Portal_Aluno.Application.Features.TurmaFeature.Queries.GetAllTurmas;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.TurmaFeature;

public class GetAllTurmasQueryHandlerTests
{
    private readonly Mock<ITurmaRepository> _turmaRepositoryMock;
    private readonly GetAllTurmasQueryHandler _handler;

    public GetAllTurmasQueryHandlerTests()
    {
        _turmaRepositoryMock = new Mock<ITurmaRepository>();
        _handler = new GetAllTurmasQueryHandler(_turmaRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoExistemTurmas_DeveRetornarLista()
    {
        var turmas = new List<Turma>
        {
            new()
            {
                Id = 1, CursoId = 1, DisciplinaId = 10, ProfessorId = 5, Semestre = 1, Ano = 2024,
                Turno = "Noturno", Status = "Ativa",
                Curso = new Curso { Nome = "Engenharia" },
                Disciplina = new Disciplina { Nome = "Cálculo" },
                Professor = new Professor { Nome = "Dr. Silva" },
                Sala = new Sala { Numero = "101" }
            }
        };
        _turmaRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(turmas);

        var resultado = await _handler.Handle(new GetAllTurmasQuery(), CancellationToken.None);

        resultado.Should().HaveCount(1);
        resultado[0].CursoNome.Should().Be("Engenharia");
    }

    [Fact]
    public async Task Handle_QuandoNaoExistemTurmas_DeveRetornarListaVazia()
    {
        _turmaRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Turma>());

        var resultado = await _handler.Handle(new GetAllTurmasQuery(), CancellationToken.None);

        resultado.Should().BeEmpty();
    }
}

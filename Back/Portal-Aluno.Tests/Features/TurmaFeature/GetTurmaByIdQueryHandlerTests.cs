using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.TurmaFeature.Queries.GetTurmaById;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.TurmaFeature;

public class GetTurmaByIdQueryHandlerTests
{
    private readonly Mock<ITurmaRepository> _turmaRepositoryMock;
    private readonly GetTurmaByIdQueryHandler _handler;

    public GetTurmaByIdQueryHandlerTests()
    {
        _turmaRepositoryMock = new Mock<ITurmaRepository>();
        _handler = new GetTurmaByIdQueryHandler(_turmaRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoTurmaExiste_DeveRetornarTurma()
    {
        var turma = new Turma
        {
            Id = 1, CursoId = 1, DisciplinaId = 10, ProfessorId = 5,
            Curso = new Curso { Nome = "Engenharia" },
            Disciplina = new Disciplina { Nome = "Cálculo" },
            Professor = new Professor { Nome = "Dr. Silva" },
            Sala = new Sala { Numero = "101" }
        };
        _turmaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(turma);

        var resultado = await _handler.Handle(new GetTurmaByIdQuery(1), CancellationToken.None);

        resultado.Should().NotBeNull();
        resultado.CursoNome.Should().Be("Engenharia");
    }

    [Fact]
    public async Task Handle_QuandoTurmaNaoExiste_DeveLancarException()
    {
        _turmaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Turma?)null);

        var acao = () => _handler.Handle(new GetTurmaByIdQuery(999), CancellationToken.None);

        await acao.Should().ThrowAsync<Exception>();
    }
}

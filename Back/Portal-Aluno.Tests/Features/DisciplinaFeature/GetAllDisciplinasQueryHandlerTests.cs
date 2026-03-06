using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.DisciplinaFeature.Queries;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.DisciplinaFeature;

public class GetAllDisciplinasQueryHandlerTests
{
    private readonly Mock<IDisciplinaRepository> _disciplinaRepositoryMock;
    private readonly GetAllDisciplinasQueryHandler _handler;

    public GetAllDisciplinasQueryHandlerTests()
    {
        _disciplinaRepositoryMock = new Mock<IDisciplinaRepository>();
        _handler = new GetAllDisciplinasQueryHandler(_disciplinaRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoExistemDisciplinas_DeveRetornarLista()
    {
        var disciplinas = new List<Disciplina>
        {
            new() { Id = 1, Nome = "Cálculo I", CargaHoraria = 60, LimiteFaltas = 15,
                CursoDisciplinas = new List<CursoDisciplina> { new() { CursoId = 1, Semestre = 1 } } },
            new() { Id = 2, Nome = "Física I", CargaHoraria = 80, LimiteFaltas = 20,
                CursoDisciplinas = new List<CursoDisciplina> { new() { CursoId = 1, Semestre = 1 } } }
        };
        _disciplinaRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(disciplinas);

        var resultado = await _handler.Handle(new GetAllDisciplinasQuery(), CancellationToken.None);

        resultado.Should().HaveCount(2);
        resultado[0].Nome.Should().Be("Cálculo I");
        resultado[1].Nome.Should().Be("Física I");
    }

    [Fact]
    public async Task Handle_QuandoNaoExistemDisciplinas_DeveRetornarListaVazia()
    {
        _disciplinaRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Disciplina>());

        var resultado = await _handler.Handle(new GetAllDisciplinasQuery(), CancellationToken.None);

        resultado.Should().BeEmpty();
    }
}

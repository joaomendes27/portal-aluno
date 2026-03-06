using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.DisciplinaFeature.Queries;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.DisciplinaFeature;

public class GetDisciplinaByIdQueryHandlerTests
{
    private readonly Mock<IDisciplinaRepository> _disciplinaRepositoryMock;
    private readonly GetDisciplinaByIdQueryHandler _handler;

    public GetDisciplinaByIdQueryHandlerTests()
    {
        _disciplinaRepositoryMock = new Mock<IDisciplinaRepository>();
        _handler = new GetDisciplinaByIdQueryHandler(_disciplinaRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoDisciplinaExiste_DeveRetornarDisciplina()
    {
        var disciplina = new Disciplina
        {
            Id = 1, Nome = "Cálculo I", CargaHoraria = 60, LimiteFaltas = 15,
            CursoDisciplinas = new List<CursoDisciplina> { new() { CursoId = 1, Semestre = 1 } }
        };
        _disciplinaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(disciplina);

        var resultado = await _handler.Handle(new GetDisciplinaByIdQuery(1), CancellationToken.None);

        resultado.Should().NotBeNull();
        resultado.Nome.Should().Be("Cálculo I");
        resultado.CargaHoraria.Should().Be(60);
    }

    [Fact]
    public async Task Handle_QuandoDisciplinaNaoExiste_DeveRetornarNull()
    {
        _disciplinaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Disciplina?)null);

        var resultado = await _handler.Handle(new GetDisciplinaByIdQuery(999), CancellationToken.None);

        resultado.Should().BeNull();
    }
}

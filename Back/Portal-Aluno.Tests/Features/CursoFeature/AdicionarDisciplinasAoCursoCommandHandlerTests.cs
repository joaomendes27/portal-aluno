using FluentAssertions;
using MediatR;
using Moq;
using Portal_Aluno.Application.Features.CursoFeature.Commands.AdicionarDisciplinasAoCurso;
using Portal_Aluno.Application.Features.CursoFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.CursoFeature;

public class AdicionarDisciplinasAoCursoCommandHandlerTests
{
    private readonly Mock<ICursoRepository> _cursoRepositoryMock;
    private readonly Mock<IDisciplinaRepository> _disciplinaRepositoryMock;
    private readonly Mock<ICursoDisciplinaRepository> _cursoDisciplinaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AdicionarDisciplinasAoCursoCommandHandler _handler;

    public AdicionarDisciplinasAoCursoCommandHandlerTests()
    {
        _cursoRepositoryMock = new Mock<ICursoRepository>();
        _disciplinaRepositoryMock = new Mock<IDisciplinaRepository>();
        _cursoDisciplinaRepositoryMock = new Mock<ICursoDisciplinaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new AdicionarDisciplinasAoCursoCommandHandler(
            _cursoRepositoryMock.Object,
            _disciplinaRepositoryMock.Object,
            _cursoDisciplinaRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoCursoEDisciplinasExistem_DeveAdicionarComSucesso()
    {
        var curso = new Curso { Id = 1, Nome = "Engenharia" };
        var disciplina = new Disciplina { Id = 10, Nome = "Cálculo" };
        var disciplinas = new List<DisciplinaCursoItem> { new(10, 1) };
        var dto = new AdicionarDisciplinasAoCursoRequest(1, disciplinas);
        var comando = new AdicionarDisciplinasAoCursoCommand(dto);

        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(curso);
        _disciplinaRepositoryMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(disciplina);
        _cursoDisciplinaRepositoryMock.Setup(r => r.GetByCursoAndDisciplinaAsync(1, 10)).ReturnsAsync((CursoDisciplina?)null);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _cursoDisciplinaRepositoryMock.Verify(r => r.AddRangeAsync(It.Is<List<CursoDisciplina>>(l => l.Count == 1)), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoCursoNaoExiste_DeveLancarKeyNotFoundException()
    {
        var disciplinas = new List<DisciplinaCursoItem> { new(10, 1) };
        var dto = new AdicionarDisciplinasAoCursoRequest(999, disciplinas);
        var comando = new AdicionarDisciplinasAoCursoCommand(dto);

        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Curso?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task Handle_QuandoDisciplinaNaoExiste_DeveLancarKeyNotFoundException()
    {
        var curso = new Curso { Id = 1, Nome = "Engenharia" };
        var disciplinas = new List<DisciplinaCursoItem> { new(999, 1) };
        var dto = new AdicionarDisciplinasAoCursoRequest(1, disciplinas);
        var comando = new AdicionarDisciplinasAoCursoCommand(dto);

        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(curso);
        _disciplinaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Disciplina?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<KeyNotFoundException>();
    }
}

using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.TurmaFeature.Commands.UpdateTurma;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.TurmaFeature;

public class AtualizarTurmaCommandHandlerTests
{
    private readonly Mock<ITurmaRepository> _turmaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ISalaRepository> _salaRepositoryMock;
    private readonly Mock<ICursoRepository> _cursoRepositoryMock;
    private readonly Mock<IDisciplinaRepository> _disciplinaRepositoryMock;
    private readonly Mock<IProfessorRepository> _professorRepositoryMock;
    private readonly AtualizarTurmaCommandHandler _handler;

    public AtualizarTurmaCommandHandlerTests()
    {
        _turmaRepositoryMock = new Mock<ITurmaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _salaRepositoryMock = new Mock<ISalaRepository>();
        _cursoRepositoryMock = new Mock<ICursoRepository>();
        _disciplinaRepositoryMock = new Mock<IDisciplinaRepository>();
        _professorRepositoryMock = new Mock<IProfessorRepository>();
        _handler = new AtualizarTurmaCommandHandler(
            _turmaRepositoryMock.Object, _unitOfWorkMock.Object,
            _salaRepositoryMock.Object, _cursoRepositoryMock.Object,
            _disciplinaRepositoryMock.Object, _professorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoTurmaExiste_DeveAtualizarComSucesso()
    {
        var turmaExistente = new Turma { Id = 1, CursoId = 1, DisciplinaId = 10, ProfessorId = 5 };
        var dto = new TurmaRequest(2, 20, 2, 2025, "Matutino", 6, "Ativa", 35, "terça-feira", "08:00", "11:00", 2);
        var comando = new AtualizarTurmaCommand(1, dto);

        _turmaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(turmaExistente);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(new Curso { Id = 2, Nome = "Medicina" });
        _disciplinaRepositoryMock.Setup(r => r.GetByIdAsync(20)).ReturnsAsync(new Disciplina { Id = 20, Nome = "Anatomia" });
        _professorRepositoryMock.Setup(r => r.GetByIdAsync(6)).ReturnsAsync(new Professor { Id = 6, Nome = "Dr. Ana" });
        _salaRepositoryMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(new Sala { Id = 2, Numero = "202" });

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().NotBeNull();
        resultado.CursoNome.Should().Be("Medicina");
        _turmaRepositoryMock.Verify(r => r.Update(turmaExistente), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoTurmaNaoExiste_DeveLancarException()
    {
        var dto = new TurmaRequest(1, 10, 1, 2024, "Noturno", 5, "Ativa", 40, "segunda-feira", "19:00", "22:00", 1);
        var comando = new AtualizarTurmaCommand(999, dto);

        _turmaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Turma?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<Exception>();
    }
}

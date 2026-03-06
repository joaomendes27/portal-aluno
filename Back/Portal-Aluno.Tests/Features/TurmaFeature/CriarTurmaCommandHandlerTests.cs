using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.TurmaFeature.Commands.CreateTurma;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.TurmaFeature;

public class CriarTurmaCommandHandlerTests
{
    private readonly Mock<ITurmaRepository> _turmaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ISalaRepository> _salaRepositoryMock;
    private readonly Mock<ICursoRepository> _cursoRepositoryMock;
    private readonly Mock<IDisciplinaRepository> _disciplinaRepositoryMock;
    private readonly Mock<IProfessorRepository> _professorRepositoryMock;
    private readonly CriarTurmaCommandHandler _handler;

    public CriarTurmaCommandHandlerTests()
    {
        _turmaRepositoryMock = new Mock<ITurmaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _salaRepositoryMock = new Mock<ISalaRepository>();
        _cursoRepositoryMock = new Mock<ICursoRepository>();
        _disciplinaRepositoryMock = new Mock<IDisciplinaRepository>();
        _professorRepositoryMock = new Mock<IProfessorRepository>();
        _handler = new CriarTurmaCommandHandler(
            _turmaRepositoryMock.Object, _unitOfWorkMock.Object,
            _salaRepositoryMock.Object, _cursoRepositoryMock.Object,
            _disciplinaRepositoryMock.Object, _professorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoDadosValidos_DeveCriarTurmaComSucesso()
    {
        var dto = new TurmaRequest(1, 10, 1, 2024, "Noturno", 5, "Ativa", 40, "segunda-feira", "19:00", "22:00", 1);
        var comando = new CriarTurmaCommand(dto);

        _turmaRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Turma>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Curso { Id = 1, Nome = "Engenharia" });
        _disciplinaRepositoryMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(new Disciplina { Id = 10, Nome = "Cálculo" });
        _professorRepositoryMock.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(new Professor { Id = 5, Nome = "Dr. Silva" });
        _salaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Sala { Id = 1, Numero = "101" });

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().NotBeNull();
        resultado.CursoNome.Should().Be("Engenharia");
        resultado.DisciplinaNome.Should().Be("Cálculo");
        resultado.ProfessorNome.Should().Be("Dr. Silva");
        _turmaRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Turma>()), Times.Once);
    }
}

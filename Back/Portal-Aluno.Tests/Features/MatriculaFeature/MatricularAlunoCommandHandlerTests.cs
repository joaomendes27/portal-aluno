using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.MatriculaFeature.Commands.MatricularAluno;
using Portal_Aluno.Application.Features.MatriculaFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.MatriculaFeature;

public class MatricularAlunoCommandHandlerTests
{
    private readonly Mock<IMatriculaRepository> _matriculaRepositoryMock;
    private readonly Mock<IMatriculaTurmaRepository> _matriculaTurmaRepositoryMock;
    private readonly Mock<IAlunoRepository> _alunoRepositoryMock;
    private readonly Mock<ICursoRepository> _cursoRepositoryMock;
    private readonly Mock<ITurmaRepository> _turmaRepositoryMock;
    private readonly Mock<ICursoDisciplinaRepository> _cursoDisciplinaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly MatricularAlunoCommandHandler _handler;

    public MatricularAlunoCommandHandlerTests()
    {
        _matriculaRepositoryMock = new Mock<IMatriculaRepository>();
        _matriculaTurmaRepositoryMock = new Mock<IMatriculaTurmaRepository>();
        _alunoRepositoryMock = new Mock<IAlunoRepository>();
        _cursoRepositoryMock = new Mock<ICursoRepository>();
        _turmaRepositoryMock = new Mock<ITurmaRepository>();
        _cursoDisciplinaRepositoryMock = new Mock<ICursoDisciplinaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new MatricularAlunoCommandHandler(
            _matriculaRepositoryMock.Object,
            _matriculaTurmaRepositoryMock.Object,
            _alunoRepositoryMock.Object,
            _cursoRepositoryMock.Object,
            _turmaRepositoryMock.Object,
            _cursoDisciplinaRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoDadosValidos_DeveMatricularComSucesso()
    {
        var aluno = new Aluno { Ra = 100, Nome = "João" };
        var curso = new Curso { Id = 1, Nome = "Engenharia" };
        var disciplina = new Disciplina { Id = 10, Nome = "Cálculo" };
        var cursoDisciplina = new CursoDisciplina { CursoId = 1, DisciplinaId = 10, Semestre = 1, Disciplina = disciplina };
        var turma = new Turma
        {
            Id = 1, CursoId = 1, DisciplinaId = 10, Capacidade = 40,
            Disciplina = disciplina, Professor = new Professor { Nome = "Dr. Silva" },
            Sala = new Sala { Numero = "101" }, MatriculaTurmas = new List<MatriculaTurma>()
        };
        var dto = new MatricularAlunoRequest(100, 1, 1, "Noturno", 2024, "Vestibular");
        var comando = new MatricularAlunoCommand(dto);

        _alunoRepositoryMock.Setup(r => r.GetByIdAsync(100)).ReturnsAsync(aluno);
        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(curso);
        _matriculaRepositoryMock.Setup(r => r.GetByAlunoRaAsync(100)).ReturnsAsync((Matricula?)null);
        _cursoDisciplinaRepositoryMock.Setup(r => r.GetByCursoIdAsync(1)).ReturnsAsync(new List<CursoDisciplina> { cursoDisciplina });
        _turmaRepositoryMock.Setup(r => r.GetTurmasDisponiveisAsync(1, 1, "Noturno", 2024)).ReturnsAsync(new List<Turma> { turma });
        _matriculaRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Matricula>())).Returns(Task.CompletedTask);
        _matriculaTurmaRepositoryMock.Setup(r => r.AddAsync(It.IsAny<MatriculaTurma>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().NotBeNull();
        resultado.AlunoRa.Should().Be(100);
        resultado.CursoNome.Should().Be("Engenharia");
        _matriculaRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Matricula>()), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoAlunoNaoExiste_DeveLancarKeyNotFoundException()
    {
        var dto = new MatricularAlunoRequest(999, 1, 1, "Noturno", 2024, "Vestibular");
        var comando = new MatricularAlunoCommand(dto);

        _alunoRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Aluno?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task Handle_QuandoCursoNaoExiste_DeveLancarKeyNotFoundException()
    {
        var aluno = new Aluno { Ra = 100, Nome = "João" };
        var dto = new MatricularAlunoRequest(100, 999, 1, "Noturno", 2024, "Vestibular");
        var comando = new MatricularAlunoCommand(dto);

        _alunoRepositoryMock.Setup(r => r.GetByIdAsync(100)).ReturnsAsync(aluno);
        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Curso?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task Handle_QuandoAlunoJaTemMatriculaAtiva_DeveLancarInvalidOperationException()
    {
        var aluno = new Aluno { Ra = 100 };
        var curso = new Curso { Id = 1, Nome = "Engenharia" };
        var matriculaAtiva = new Matricula { Id = 1, Ra = 100, Status = "Ativa" };
        var dto = new MatricularAlunoRequest(100, 1, 1, "Noturno", 2024, "Vestibular");
        var comando = new MatricularAlunoCommand(dto);

        _alunoRepositoryMock.Setup(r => r.GetByIdAsync(100)).ReturnsAsync(aluno);
        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(curso);
        _matriculaRepositoryMock.Setup(r => r.GetByAlunoRaAsync(100)).ReturnsAsync(matriculaAtiva);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<InvalidOperationException>();
    }
}

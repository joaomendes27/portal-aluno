using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.MatriculaFeature.Commands.InscreverEmTurma;
using Portal_Aluno.Application.Features.MatriculaFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.MatriculaFeature;

public class InscreverEmTurmaCommandHandlerTests
{
    private readonly Mock<IMatriculaTurmaRepository> _matriculaTurmaRepositoryMock;
    private readonly Mock<IMatriculaRepository> _matriculaRepositoryMock;
    private readonly Mock<ITurmaRepository> _turmaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly InscreverEmTurmaCommandHandler _handler;

    public InscreverEmTurmaCommandHandlerTests()
    {
        _matriculaTurmaRepositoryMock = new Mock<IMatriculaTurmaRepository>();
        _matriculaRepositoryMock = new Mock<IMatriculaRepository>();
        _turmaRepositoryMock = new Mock<ITurmaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new InscreverEmTurmaCommandHandler(
            _matriculaTurmaRepositoryMock.Object,
            _matriculaRepositoryMock.Object,
            _turmaRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoDadosValidos_DeveInscreverComSucesso()
    {
        var matricula = new Matricula { Id = 1, CursoId = 1 };
        var turma = new Turma { Id = 10, CursoId = 1 };
        var dto = new InscreverEmTurmaRequest(1, 10);
        var comando = new InscreverEmTurmaCommand(dto);

        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matricula);
        _turmaRepositoryMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(turma);
        _matriculaTurmaRepositoryMock.Setup(r => r.GetByMatriculaAndTurmaAsync(1, 10)).ReturnsAsync((MatriculaTurma?)null);
        _matriculaTurmaRepositoryMock.Setup(r => r.AddAsync(It.IsAny<MatriculaTurma>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        _matriculaTurmaRepositoryMock.Verify(r => r.AddAsync(It.Is<MatriculaTurma>(mt =>
            mt.MatriculaId == 1 && mt.TurmaId == 10 && mt.Situacao == "Cursando")), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoMatriculaNaoExiste_DeveLancarKeyNotFoundException()
    {
        var dto = new InscreverEmTurmaRequest(999, 10);
        var comando = new InscreverEmTurmaCommand(dto);

        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Matricula?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task Handle_QuandoTurmaNaoExiste_DeveLancarKeyNotFoundException()
    {
        var matricula = new Matricula { Id = 1, CursoId = 1 };
        var dto = new InscreverEmTurmaRequest(1, 999);
        var comando = new InscreverEmTurmaCommand(dto);

        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matricula);
        _turmaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Turma?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task Handle_QuandoTurmaNaoPertenceAoCurso_DeveLancarInvalidOperationException()
    {
        var matricula = new Matricula { Id = 1, CursoId = 1 };
        var turma = new Turma { Id = 10, CursoId = 2 };
        var dto = new InscreverEmTurmaRequest(1, 10);
        var comando = new InscreverEmTurmaCommand(dto);

        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matricula);
        _turmaRepositoryMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(turma);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Handle_QuandoAlunoJaInscrito_DeveLancarInvalidOperationException()
    {
        var matricula = new Matricula { Id = 1, CursoId = 1 };
        var turma = new Turma { Id = 10, CursoId = 1 };
        var inscricaoExistente = new MatriculaTurma { MatriculaId = 1, TurmaId = 10 };
        var dto = new InscreverEmTurmaRequest(1, 10);
        var comando = new InscreverEmTurmaCommand(dto);

        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matricula);
        _turmaRepositoryMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(turma);
        _matriculaTurmaRepositoryMock.Setup(r => r.GetByMatriculaAndTurmaAsync(1, 10)).ReturnsAsync(inscricaoExistente);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<InvalidOperationException>();
    }
}

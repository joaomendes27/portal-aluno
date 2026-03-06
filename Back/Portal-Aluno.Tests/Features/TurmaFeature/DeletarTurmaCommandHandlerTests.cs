using FluentAssertions;
using MediatR;
using Moq;
using Portal_Aluno.Application.Features.TurmaFeature.Commands.DeleteTurma;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.TurmaFeature;

public class DeletarTurmaCommandHandlerTests
{
    private readonly Mock<ITurmaRepository> _turmaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeletarTurmaCommandHandler _handler;

    public DeletarTurmaCommandHandlerTests()
    {
        _turmaRepositoryMock = new Mock<ITurmaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeletarTurmaCommandHandler(_turmaRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoTurmaExiste_DeveDeletarComSucesso()
    {
        var turma = new Turma { Id = 1 };
        var comando = new DeletarTurmaCommand(1);

        _turmaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(turma);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _turmaRepositoryMock.Verify(r => r.Delete(turma), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoTurmaNaoExiste_DeveRetornarSemErro()
    {
        var comando = new DeletarTurmaCommand(999);
        _turmaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Turma?)null);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _turmaRepositoryMock.Verify(r => r.Delete(It.IsAny<Turma>()), Times.Never);
    }
}

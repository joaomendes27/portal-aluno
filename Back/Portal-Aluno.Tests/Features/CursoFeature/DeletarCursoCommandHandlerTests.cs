using FluentAssertions;
using MediatR;
using Moq;
using Portal_Aluno.Application.Features.CursoFeature.Commands.DeletarCurso;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.CursoFeature;

public class DeletarCursoCommandHandlerTests
{
    private readonly Mock<ICursoRepository> _cursoRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeletarCursoCommandHandler _handler;

    public DeletarCursoCommandHandlerTests()
    {
        _cursoRepositoryMock = new Mock<ICursoRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeletarCursoCommandHandler(_cursoRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoCursoExiste_DeveDeletarComSucesso()
    {
        var curso = new Curso { Id = 1, Nome = "Engenharia" };
        var comando = new DeletarCursoCommand(1);

        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(curso);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _cursoRepositoryMock.Verify(r => r.Delete(curso), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoCursoNaoExiste_DeveLancarKeyNotFoundException()
    {
        var comando = new DeletarCursoCommand(999);
        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Curso?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<KeyNotFoundException>();
    }
}

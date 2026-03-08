using FluentAssertions;
using MediatR;
using Moq;
using Portal_Aluno.Application.Features.SalaFeature.Commands.DeleteSala;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.SalaFeature;

public class DeletarSalaCommandHandlerTests
{
    private readonly Mock<ISalaRepository> _salaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeletarSalaCommandHandler _handler;

    public DeletarSalaCommandHandlerTests()
    {
        _salaRepositoryMock = new Mock<ISalaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeletarSalaCommandHandler(_salaRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoSalaExiste_DeveDeletarComSucesso()
    {
        var sala = new Sala { Id = 1, Andar = 2, Numero = "201" };
        var comando = new DeletarSalaCommand(1);

        _salaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(sala);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _salaRepositoryMock.Verify(r => r.Delete(sala), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoSalaNaoExiste_DeveRetornarSemErro()
    {
        var comando = new DeletarSalaCommand(999);
        _salaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Sala?)null);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _salaRepositoryMock.Verify(r => r.Delete(It.IsAny<Sala>()), Times.Never);
    }
}

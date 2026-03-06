using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.SalaFeature.Commands.UpdateSala;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.SalaFeature;

public class AtualizarSalaCommandHandlerTests
{
    private readonly Mock<ISalaRepository> _salaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AtualizarSalaCommandHandler _handler;

    public AtualizarSalaCommandHandlerTests()
    {
        _salaRepositoryMock = new Mock<ISalaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new AtualizarSalaCommandHandler(_salaRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoSalaExiste_DeveAtualizarComSucesso()
    {
        var salaExistente = new Sala { Id = 1, Andar = 1, Numero = "100" };
        var dto = new SalaRequest(3, "301B");
        var comando = new AtualizarSalaCommand(1, dto);

        _salaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(salaExistente);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().NotBeNull();
        resultado.Andar.Should().Be(3);
        resultado.Numero.Should().Be("301B");
        _salaRepositoryMock.Verify(r => r.Update(salaExistente), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoSalaNaoExiste_DeveLancarException()
    {
        var dto = new SalaRequest(1, "100");
        var comando = new AtualizarSalaCommand(999, dto);

        _salaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Sala?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<Exception>();
    }
}

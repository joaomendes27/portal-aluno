using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.SalaFeature.Commands.CreateSala;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.SalaFeature;

public class CriarSalaCommandHandlerTests
{
    private readonly Mock<ISalaRepository> _salaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CriarSalaCommandHandler _handler;

    public CriarSalaCommandHandlerTests()
    {
        _salaRepositoryMock = new Mock<ISalaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CriarSalaCommandHandler(_salaRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoDadosValidos_DeveCriarSalaComSucesso()
    {
        var dto = new SalaRequest(2, "101A");
        var comando = new CriarSalaCommand(dto);

        _salaRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Sala>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().NotBeNull();
        resultado.Andar.Should().Be(2);
        resultado.Numero.Should().Be("101A");
        _salaRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Sala>()), Times.Once);
    }
}

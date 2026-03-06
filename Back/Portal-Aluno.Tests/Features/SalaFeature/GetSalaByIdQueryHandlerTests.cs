using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.SalaFeature.Queries;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.SalaFeature;

public class GetSalaByIdQueryHandlerTests
{
    private readonly Mock<ISalaRepository> _salaRepositoryMock;
    private readonly GetSalaByIdQueryHandler _handler;

    public GetSalaByIdQueryHandlerTests()
    {
        _salaRepositoryMock = new Mock<ISalaRepository>();
        _handler = new GetSalaByIdQueryHandler(_salaRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoSalaExiste_DeveRetornarSala()
    {
        var sala = new Sala { Id = 1, Andar = 3, Numero = "301" };
        _salaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(sala);

        var resultado = await _handler.Handle(new GetSalaByIdQuery(1), CancellationToken.None);

        resultado.Should().NotBeNull();
        resultado.Id.Should().Be(1);
        resultado.Numero.Should().Be("301");
    }

    [Fact]
    public async Task Handle_QuandoSalaNaoExiste_DeveLancarException()
    {
        _salaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Sala?)null);

        var acao = () => _handler.Handle(new GetSalaByIdQuery(999), CancellationToken.None);

        await acao.Should().ThrowAsync<Exception>();
    }
}

using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;
using Portal_Aluno.Application.Features.SalaFeature.Queries;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.SalaFeature;

public class GetAllSalasQueryHandlerTests
{
    private readonly Mock<ISalaRepository> _salaRepositoryMock;
    private readonly GetAllSalasQueryHandler _handler;

    public GetAllSalasQueryHandlerTests()
    {
        _salaRepositoryMock = new Mock<ISalaRepository>();
        _handler = new GetAllSalasQueryHandler(_salaRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoExistemSalas_DeveRetornarLista()
    {
        var salas = new List<Sala>
        {
            new() { Id = 1, Andar = 1, Numero = "101" },
            new() { Id = 2, Andar = 2, Numero = "202" }
        };
        _salaRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(salas);

        var resultado = await _handler.Handle(new GetAllSalasQuery(), CancellationToken.None);

        resultado.Should().HaveCount(2);
        resultado[0].Numero.Should().Be("101");
        resultado[1].Numero.Should().Be("202");
    }

    [Fact]
    public async Task Handle_QuandoNaoExistemSalas_DeveRetornarListaVazia()
    {
        _salaRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Sala>());

        var resultado = await _handler.Handle(new GetAllSalasQuery(), CancellationToken.None);

        resultado.Should().BeEmpty();
    }
}

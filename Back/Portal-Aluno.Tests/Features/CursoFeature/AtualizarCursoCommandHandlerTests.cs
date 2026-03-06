using FluentAssertions;
using MediatR;
using Moq;
using Portal_Aluno.Application.Features.CursoFeature.Commands.AtualizarCurso;
using Portal_Aluno.Application.Features.CursoFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.CursoFeature;

public class AtualizarCursoCommandHandlerTests
{
    private readonly Mock<ICursoRepository> _cursoRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AtualizarCursoCommandHandler _handler;

    public AtualizarCursoCommandHandlerTests()
    {
        _cursoRepositoryMock = new Mock<ICursoRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new AtualizarCursoCommandHandler(_cursoRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoCursoExiste_DeveAtualizarComSucesso()
    {
        var cursoExistente = new Curso { Id = 1, Nome = "Antigo", Grau = "Bacharelado", CargaHoraria = 3000 };
        var dto = new AtualizarCursoRequest("Engenharia Atualizada", "Licenciatura", 4000);
        var comando = new AtualizarCursoCommand(1, dto);

        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(cursoExistente);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        cursoExistente.Nome.Should().Be("Engenharia Atualizada");
        cursoExistente.Grau.Should().Be("Licenciatura");
        cursoExistente.CargaHoraria.Should().Be(4000);
        _cursoRepositoryMock.Verify(r => r.Update(cursoExistente), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoCursoNaoExiste_DeveLancarKeyNotFoundException()
    {
        var dto = new AtualizarCursoRequest("Teste", "Bacharelado", 3000);
        var comando = new AtualizarCursoCommand(999, dto);

        _cursoRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Curso?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<KeyNotFoundException>();
    }
}

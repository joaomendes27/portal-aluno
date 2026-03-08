using FluentAssertions;
using MediatR;
using Moq;
using Portal_Aluno.Application.Features.CursoFeature.Commands.RemoverDisciplinaDoCurso;
using Portal_Aluno.Application.Features.CursoFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.CursoFeature;

public class RemoverDisciplinaDoCursoCommandHandlerTests
{
    private readonly Mock<ICursoDisciplinaRepository> _cursoDisciplinaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly RemoverDisciplinaDoCursoCommandHandler _handler;

    public RemoverDisciplinaDoCursoCommandHandlerTests()
    {
        _cursoDisciplinaRepositoryMock = new Mock<ICursoDisciplinaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new RemoverDisciplinaDoCursoCommandHandler(
            _cursoDisciplinaRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoVinculoExiste_DeveRemoverComSucesso()
    {
        var vinculo = new CursoDisciplina { CursoId = 1, DisciplinaId = 10 };
        var dto = new RemoverDisciplinaDoCursoRequest(1, 10);
        var comando = new RemoverDisciplinaDoCursoCommand(dto);

        _cursoDisciplinaRepositoryMock.Setup(r => r.GetByCursoAndDisciplinaAsync(1, 10)).ReturnsAsync(vinculo);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _cursoDisciplinaRepositoryMock.Verify(r => r.Delete(vinculo), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoVinculoNaoExiste_DeveLancarKeyNotFoundException()
    {
        var dto = new RemoverDisciplinaDoCursoRequest(1, 999);
        var comando = new RemoverDisciplinaDoCursoCommand(dto);

        _cursoDisciplinaRepositoryMock.Setup(r => r.GetByCursoAndDisciplinaAsync(1, 999)).ReturnsAsync((CursoDisciplina?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<KeyNotFoundException>();
    }
}

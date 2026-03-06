using FluentAssertions;
using MediatR;
using Moq;
using Features.DisciplinaFeature.Commands.DeleteDisciplina;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.DisciplinaFeature;

public class DeletarDisciplinaCommandHandlerTests
{
    private readonly Mock<IDisciplinaRepository> _disciplinaRepositoryMock;
    private readonly Mock<ICursoDisciplinaRepository> _cursoDisciplinaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeletarDisciplinaCommandHandler _handler;

    public DeletarDisciplinaCommandHandlerTests()
    {
        _disciplinaRepositoryMock = new Mock<IDisciplinaRepository>();
        _cursoDisciplinaRepositoryMock = new Mock<ICursoDisciplinaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeletarDisciplinaCommandHandler(
            _disciplinaRepositoryMock.Object,
            _cursoDisciplinaRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoDisciplinaTemVinculos_DeveRemoverVinculosEDeletar()
    {
        var vinculos = new List<CursoDisciplina>
        {
            new() { CursoId = 1, DisciplinaId = 5 },
            new() { CursoId = 2, DisciplinaId = 5 }
        };
        var comando = new DeletarDisciplinaCommand(5);

        _cursoDisciplinaRepositoryMock.Setup(r => r.GetByDisciplinaIdAsync(5)).ReturnsAsync(vinculos);
        _disciplinaRepositoryMock.Setup(r => r.DeleteAsync(5)).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _cursoDisciplinaRepositoryMock.Verify(r => r.DeleteRange(vinculos), Times.Once);
        _disciplinaRepositoryMock.Verify(r => r.DeleteAsync(5), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoDisciplinaSemVinculos_DeveDeletarSemRemoverVinculos()
    {
        var comando = new DeletarDisciplinaCommand(5);

        _cursoDisciplinaRepositoryMock.Setup(r => r.GetByDisciplinaIdAsync(5)).ReturnsAsync(new List<CursoDisciplina>());
        _disciplinaRepositoryMock.Setup(r => r.DeleteAsync(5)).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _cursoDisciplinaRepositoryMock.Verify(r => r.DeleteRange(It.IsAny<List<CursoDisciplina>>()), Times.Never);
        _disciplinaRepositoryMock.Verify(r => r.DeleteAsync(5), Times.Once);
    }
}

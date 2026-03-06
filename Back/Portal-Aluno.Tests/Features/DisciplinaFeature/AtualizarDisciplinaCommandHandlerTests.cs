using FluentAssertions;
using MediatR;
using Moq;
using Features.DisciplinaFeature.Commands.UpdateDisciplina;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.DisciplinaFeature;

public class AtualizarDisciplinaCommandHandlerTests
{
    private readonly Mock<IDisciplinaRepository> _disciplinaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AtualizarDisciplinaCommandHandler _handler;

    public AtualizarDisciplinaCommandHandlerTests()
    {
        _disciplinaRepositoryMock = new Mock<IDisciplinaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new AtualizarDisciplinaCommandHandler(_disciplinaRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoDisciplinaExiste_DeveAtualizarComSucesso()
    {
        var disciplina = new Disciplina { Id = 1, Nome = "Cálculo I", CargaHoraria = 60, LimiteFaltas = 15 };
        var request = new DisciplinaRequest { CursoId = 1, Nome = "Cálculo II", CargaHoraria = 80, LimiteFaltas = 20 };
        var comando = new AtualizarDisciplinaCommand(1, request);

        _disciplinaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(disciplina);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        disciplina.Nome.Should().Be("Cálculo II");
        disciplina.CargaHoraria.Should().Be(80);
        disciplina.LimiteFaltas.Should().Be(20);
    }

    [Fact]
    public async Task Handle_QuandoDisciplinaNaoExiste_DeveLancarException()
    {
        var request = new DisciplinaRequest { Nome = "Teste", CargaHoraria = 60, LimiteFaltas = 15 };
        var comando = new AtualizarDisciplinaCommand(999, request);

        _disciplinaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Disciplina?)null);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<Exception>();
    }
}

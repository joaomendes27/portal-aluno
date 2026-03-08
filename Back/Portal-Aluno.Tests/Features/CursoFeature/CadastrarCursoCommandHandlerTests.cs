using FluentAssertions;
using Moq;
using Portal_Aluno.Application.Features.CursoFeature.Commands.CadastrarCurso;
using Portal_Aluno.Application.Features.CursoFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.CursoFeature;

public class CadastrarCursoCommandHandlerTests
{
    private readonly Mock<ICursoRepository> _cursoRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CadastrarCursoCommandHandler _handler;

    public CadastrarCursoCommandHandlerTests()
    {
        _cursoRepositoryMock = new Mock<ICursoRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CadastrarCursoCommandHandler(_cursoRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoDadosValidos_DeveCadastrarCursoComSucesso()
    {
        var dto = new CadastrarCursoRequest("Engenharia de Software", "Bacharelado", 3600);
        var comando = new CadastrarCursoCommand(dto);

        _cursoRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Curso>()))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        _cursoRepositoryMock.Verify(r => r.AddAsync(It.Is<Curso>(c =>
            c.Nome == "Engenharia de Software" &&
            c.Grau == "Bacharelado" &&
            c.CargaHoraria == 3600)), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoChamado_DeveRetornarIdDoCurso()
    {
        var dto = new CadastrarCursoRequest("Medicina", "Bacharelado", 7200);
        var comando = new CadastrarCursoCommand(dto);

        _cursoRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Curso>()))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().BeGreaterThanOrEqualTo(0);
    }
}

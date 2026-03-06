using FluentAssertions;
using MediatR;
using Moq;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAdmin;
using Portal_Aluno.Application.Features.UsuarioFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.UsuarioFeature;

public class CadastrarAdminCommandHandlerTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CadastrarAdminCommandHandler _handler;

    public CadastrarAdminCommandHandlerTests()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CadastrarAdminCommandHandler(_usuarioRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoEmailNaoExiste_DeveCadastrarComSucesso()
    {
        var dto = new CadastrarAdminRequest { Nome = "Admin", Email = "admin@teste.com", Senha = "Senha123" };
        var comando = new CadastrarAdminCommand(dto);

        _usuarioRepositoryMock.Setup(r => r.LoginExistsAsync("admin@teste.com")).ReturnsAsync(false);
        _usuarioRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _usuarioRepositoryMock.Verify(r => r.AddAsync(It.Is<Usuario>(u =>
            u.Login == "admin@teste.com" && u.Tipo == "adm")), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoEmailJaExiste_DeveLancarException()
    {
        var dto = new CadastrarAdminRequest { Nome = "Admin", Email = "admin@teste.com", Senha = "Senha123" };
        var comando = new CadastrarAdminCommand(dto);

        _usuarioRepositoryMock.Setup(r => r.LoginExistsAsync("admin@teste.com")).ReturnsAsync(true);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<Exception>();
    }
}

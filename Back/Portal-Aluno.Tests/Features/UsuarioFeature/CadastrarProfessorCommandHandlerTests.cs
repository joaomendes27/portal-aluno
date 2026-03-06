using FluentAssertions;
using MediatR;
using Moq;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarProfessor;
using Portal_Aluno.Application.Features.UsuarioFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.UsuarioFeature;

public class CadastrarProfessorCommandHandlerTests
{
    private readonly Mock<IProfessorRepository> _professorRepositoryMock;
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CadastrarProfessorCommandHandler _handler;

    public CadastrarProfessorCommandHandlerTests()
    {
        _professorRepositoryMock = new Mock<IProfessorRepository>();
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CadastrarProfessorCommandHandler(
            _professorRepositoryMock.Object,
            _usuarioRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoProfessorNovo_DeveCadastrarComSucesso()
    {
        var dto = new CadastrarProfessorRequest
        {
            Nome = "Dr. Silva", Email = "silva@email.com",
            Cpf = "12345678901", Titulo = "Doutor", Senha = "Senha123"
        };
        var comando = new CadastrarProfessorCommand(dto);

        _professorRepositoryMock.Setup(r => r.GetByCpfAsync("12345678901")).ReturnsAsync((Professor?)null);
        _professorRepositoryMock.Setup(r => r.GetByEmailAsync("silva@email.com")).ReturnsAsync((Professor?)null);
        _usuarioRepositoryMock.Setup(r => r.LoginExistsAsync("silva@email.com")).ReturnsAsync(false);
        _professorRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Professor>())).Returns(Task.CompletedTask);
        _usuarioRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _professorRepositoryMock.Verify(r => r.AddAsync(It.Is<Professor>(p =>
            p.Nome == "Dr. Silva" && p.Status == "Ativo")), Times.Once);
        _usuarioRepositoryMock.Verify(r => r.AddAsync(It.Is<Usuario>(u =>
            u.Tipo == "professor" && u.Login == "silva@email.com")), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoCpfJaCadastradoParaProfessorAtivo_DeveLancarException()
    {
        var professorAtivo = new Professor { Id = 1, Cpf = "12345678901", Status = "Ativo" };
        var dto = new CadastrarProfessorRequest
        {
            Nome = "Dr. Silva", Email = "silva@email.com",
            Cpf = "12345678901", Titulo = "Doutor", Senha = "Senha123"
        };
        var comando = new CadastrarProfessorCommand(dto);

        _professorRepositoryMock.Setup(r => r.GetByCpfAsync("12345678901")).ReturnsAsync(professorAtivo);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<Exception>().WithMessage("*CPF*");
    }

    [Fact]
    public async Task Handle_QuandoEmailJaCadastradoParaProfessorAtivo_DeveLancarException()
    {
        var professorAtivo = new Professor { Id = 1, Email = "silva@email.com", Status = "Ativo" };
        var dto = new CadastrarProfessorRequest
        {
            Nome = "Dr. Silva", Email = "silva@email.com",
            Cpf = "99999999999", Titulo = "Doutor", Senha = "Senha123"
        };
        var comando = new CadastrarProfessorCommand(dto);

        _professorRepositoryMock.Setup(r => r.GetByCpfAsync("99999999999")).ReturnsAsync((Professor?)null);
        _professorRepositoryMock.Setup(r => r.GetByEmailAsync("silva@email.com")).ReturnsAsync(professorAtivo);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<Exception>().WithMessage("*Email*");
    }
}

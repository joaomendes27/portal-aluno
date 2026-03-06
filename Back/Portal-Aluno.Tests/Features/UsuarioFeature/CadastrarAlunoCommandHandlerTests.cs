using FluentAssertions;
using MediatR;
using Moq;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAluno;
using Portal_Aluno.Application.Features.UsuarioFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Tests.Features.UsuarioFeature;

public class CadastrarAlunoCommandHandlerTests
{
    private readonly Mock<IAlunoRepository> _alunoRepositoryMock;
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CadastrarAlunoCommandHandler _handler;

    public CadastrarAlunoCommandHandlerTests()
    {
        _alunoRepositoryMock = new Mock<IAlunoRepository>();
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CadastrarAlunoCommandHandler(
            _alunoRepositoryMock.Object,
            _usuarioRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoAlunoNovo_DeveCadastrarComSucesso()
    {
        var dto = new CadastrarAlunoRequest
        {
            Nome = "João Silva", Email = "joao@email.com", Cpf = "12345678901",
            DataNascimento = new DateTime(2000, 1, 1), Cep = "01001000",
            Rua = "Rua Teste", Numero = "100", Bairro = "Centro",
            Cidade = "São Paulo", Estado = "SP", Pais = "Brasil", Senha = "Senha123"
        };
        var comando = new CadastrarAlunoCommand(dto);

        _alunoRepositoryMock.Setup(r => r.GetByCpfAsync("12345678901")).ReturnsAsync((Aluno?)null);
        _alunoRepositoryMock.Setup(r => r.GetByEmailAsync("joao@email.com")).ReturnsAsync((Aluno?)null);
        _alunoRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Aluno>())).Returns(Task.CompletedTask);
        _usuarioRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Should().Be(Unit.Value);
        _alunoRepositoryMock.Verify(r => r.AddAsync(It.Is<Aluno>(a =>
            a.Nome == "João Silva" && a.Status == "Ativo")), Times.Once);
        _usuarioRepositoryMock.Verify(r => r.AddAsync(It.Is<Usuario>(u =>
            u.Tipo == "aluno")), Times.Once);
    }

    [Fact]
    public async Task Handle_QuandoCpfJaCadastradoParaAlunoAtivo_DeveLancarException()
    {
        var alunoAtivo = new Aluno { Ra = 1, Cpf = "12345678901", Status = "Ativo" };
        var dto = new CadastrarAlunoRequest
        {
            Nome = "João", Email = "joao@email.com", Cpf = "12345678901",
            DataNascimento = new DateTime(2000, 1, 1), Cep = "01001000",
            Rua = "Rua", Numero = "1", Bairro = "Centro",
            Cidade = "SP", Estado = "SP", Pais = "Brasil", Senha = "Senha123"
        };
        var comando = new CadastrarAlunoCommand(dto);

        _alunoRepositoryMock.Setup(r => r.GetByCpfAsync("12345678901")).ReturnsAsync(alunoAtivo);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<Exception>().WithMessage("*CPF*");
    }

    [Fact]
    public async Task Handle_QuandoEmailJaCadastradoParaAlunoAtivo_DeveLancarException()
    {
        var alunoAtivo = new Aluno { Ra = 1, Email = "joao@email.com", Status = "Ativo" };
        var dto = new CadastrarAlunoRequest
        {
            Nome = "João", Email = "joao@email.com", Cpf = "99999999999",
            DataNascimento = new DateTime(2000, 1, 1), Cep = "01001000",
            Rua = "Rua", Numero = "1", Bairro = "Centro",
            Cidade = "SP", Estado = "SP", Pais = "Brasil", Senha = "Senha123"
        };
        var comando = new CadastrarAlunoCommand(dto);

        _alunoRepositoryMock.Setup(r => r.GetByCpfAsync("99999999999")).ReturnsAsync((Aluno?)null);
        _alunoRepositoryMock.Setup(r => r.GetByEmailAsync("joao@email.com")).ReturnsAsync(alunoAtivo);

        var acao = () => _handler.Handle(comando, CancellationToken.None);

        await acao.Should().ThrowAsync<Exception>().WithMessage("*Email*");
    }
}

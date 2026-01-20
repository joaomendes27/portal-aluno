using MediatR;
using Portal_Aluno.Application.Features.UsuarioFeature.DTOs;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.LoginUsuario;

public class LoginUsuarioCommand : IRequest<LoginResponse>
{
    public string Login { get; set; }
    public string Senha { get; set; }

    public LoginUsuarioCommand(LoginRequest dto)
    {
        Login = dto.Login;
        Senha = dto.Senha;
    }
}

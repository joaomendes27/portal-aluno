using MediatR;
using Portal_Aluno.Application.Features.UsuarioFeature.DTOs;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarUsuario;

public class CadastrarUsuarioCommand : IRequest<Unit>
{
    public CadastrarUsuarioRequest Dto { get; }

    public CadastrarUsuarioCommand(CadastrarUsuarioRequest dto)
    {
        Dto = dto;
    }
}
        
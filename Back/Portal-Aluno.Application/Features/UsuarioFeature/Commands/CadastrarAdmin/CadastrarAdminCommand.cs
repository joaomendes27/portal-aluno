using MediatR;
using Portal_Aluno.Application.Features.UsuarioFeature.DTOs;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAdmin;

public class CadastrarAdminCommand : IRequest<Unit>
{
    public CadastrarAdminRequest Dto { get; }

    public CadastrarAdminCommand(CadastrarAdminRequest dto)
    {
        Dto = dto;
    }
}

using MediatR;
using Portal_Aluno.Application.Features.UsuarioFeature.DTOs;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarProfessor;

public class CadastrarProfessorCommand : IRequest<Unit>
{
    public CadastrarProfessorRequest Dto { get; }

    public CadastrarProfessorCommand(CadastrarProfessorRequest dto)
    {
        Dto = dto;
    }
}

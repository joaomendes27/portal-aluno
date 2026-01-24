using MediatR;
using Portal_Aluno.Application.Features.UsuarioFeature.DTOs;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAluno;

public class CadastrarAlunoCommand : IRequest<Unit>
{
    public CadastrarAlunoRequest Dto { get; }

    public CadastrarAlunoCommand(CadastrarAlunoRequest dto)
    {
        Dto = dto;
    }
}

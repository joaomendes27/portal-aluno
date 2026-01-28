using MediatR;
using Portal_Aluno.Application.Features.CursoFeature.DTOs;

namespace Portal_Aluno.Application.Features.CursoFeature.Commands;

public class CadastrarCursoCommand : IRequest<int>
{
    public CadastrarCursoRequest Dto { get; }

    public CadastrarCursoCommand(CadastrarCursoRequest dto)
    {
        Dto = dto;
    }
}

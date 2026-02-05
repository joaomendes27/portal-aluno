using MediatR;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;

namespace Portal_Aluno.Application.Features.TurmaFeature.Commands.CreateTurma;

public class CriarTurmaCommand : IRequest<TurmaResponse>
{
    public TurmaRequest Dto { get; }

    public CriarTurmaCommand(TurmaRequest dto)
    {
        Dto = dto;
    }
}

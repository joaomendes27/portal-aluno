using MediatR;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;

namespace Portal_Aluno.Application.Features.TurmaFeature.Commands.CreateTurma;

public class CreateTurmaCommand : IRequest<TurmaResponse>
{
    public TurmaRequest Dto { get; }

    public CreateTurmaCommand(TurmaRequest dto)
    {
        Dto = dto;
    }
}

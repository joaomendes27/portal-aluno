using MediatR;
using Portal_Aluno.Application.Features.MatriculaFeature.DTOs;

namespace Portal_Aluno.Application.Features.MatriculaFeature.Commands.InscreverEmTurma;

public class InscreverEmTurmaCommand : IRequest<int>
{
    public InscreverEmTurmaRequest Dto { get; }

    public InscreverEmTurmaCommand(InscreverEmTurmaRequest dto)
    {
        Dto = dto;
    }
}

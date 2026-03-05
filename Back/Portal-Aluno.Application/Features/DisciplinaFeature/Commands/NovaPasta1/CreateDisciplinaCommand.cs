using MediatR;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;

namespace Portal_Aluno.Application.Features.DisciplinaFeature.Commands;

public class CreateDisciplinaCommand : IRequest<int>
{
    public DisciplinaRequest Request { get; set; }
    public CreateDisciplinaCommand(DisciplinaRequest request)
    {
        Request = request;
    }
}

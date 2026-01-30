using MediatR;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;

namespace Features.DisciplinaFeature.Commands.CreateDisciplina;

public class CreateDisciplinaCommand : IRequest<int>
{
    public DisciplinaRequest Request { get; set; }
    public CreateDisciplinaCommand(DisciplinaRequest request)
    {
        Request = request;
    }
}

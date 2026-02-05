using MediatR;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;

namespace Features.DisciplinaFeature.Commands.CreateDisciplina;

public class CriarDisciplinaCommand : IRequest<int>
{
    public DisciplinaRequest Request { get; set; }
    public CriarDisciplinaCommand(DisciplinaRequest request)
    {
        Request = request;
    }
}

using MediatR;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;

namespace Portal_Aluno.Application.Features.DisciplinaFeature.Commands;

public class UpdateDisciplinaCommand : IRequest<Unit>
{
    public int Id { get; }
    public DisciplinaRequest Request { get; }

    public UpdateDisciplinaCommand(int id, DisciplinaRequest request)
    {
        Id = id;
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
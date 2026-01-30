using MediatR;

namespace Portal_Aluno.Application.Features.DisciplinaFeature.Commands;

public class DeleteDisciplinaCommand : IRequest<Unit>
{
    public int Id { get; }

    public DeleteDisciplinaCommand(int id)
    {
        Id = id;
    }
}
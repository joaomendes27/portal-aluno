using MediatR;

namespace Features.DisciplinaFeature.Commands.DeleteDisciplina;

public class DeleteDisciplinaCommand : IRequest<Unit>
{
    public int Id { get; }

    public DeleteDisciplinaCommand(int id)
    {
        Id = id;
    }
}
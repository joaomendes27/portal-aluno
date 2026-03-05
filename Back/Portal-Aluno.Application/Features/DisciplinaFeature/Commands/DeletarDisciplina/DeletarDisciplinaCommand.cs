using MediatR;

namespace Features.DisciplinaFeature.Commands.DeleteDisciplina;

public class DeletarDisciplinaCommand : IRequest<Unit>
{
    public int Id { get; }

    public DeletarDisciplinaCommand(int id)
    {
        Id = id;
    }
}
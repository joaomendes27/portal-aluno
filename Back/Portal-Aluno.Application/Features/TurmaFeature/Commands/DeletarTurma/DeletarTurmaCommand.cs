using MediatR;

namespace Portal_Aluno.Application.Features.TurmaFeature.Commands.DeleteTurma;

public class DeletarTurmaCommand : IRequest<Unit>
{
    public int Id { get; }

    public DeletarTurmaCommand(int id)
    {
        Id = id;
    }
}
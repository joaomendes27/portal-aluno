using MediatR;

namespace Portal_Aluno.Application.Features.TurmaFeature.Commands.DeleteTurma;

public class DeleteTurmaCommand : IRequest<Unit>
{
    public int Id { get; }

    public DeleteTurmaCommand(int id)
    {
        Id = id;
    }
}
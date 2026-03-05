using MediatR;

namespace Portal_Aluno.Application.Features.CursoFeature.Commands.DeletarCurso;

public class DeletarCursoCommand : IRequest<Unit>
{
    public int Id { get; }

    public DeletarCursoCommand(int id)
    {
        Id = id;
    }
}

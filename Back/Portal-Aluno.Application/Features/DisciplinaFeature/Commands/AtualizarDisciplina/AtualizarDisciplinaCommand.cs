using MediatR;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;

namespace Features.DisciplinaFeature.Commands.UpdateDisciplina;

public class AtualizarDisciplinaCommand : IRequest<Unit>
{
    public int Id { get; }
    public DisciplinaRequest Request { get; }

    public AtualizarDisciplinaCommand(int id, DisciplinaRequest request)
    {
        Id = id;
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
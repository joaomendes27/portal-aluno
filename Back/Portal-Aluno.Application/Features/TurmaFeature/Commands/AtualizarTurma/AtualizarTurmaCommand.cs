using MediatR;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;

namespace Portal_Aluno.Application.Features.TurmaFeature.Commands.UpdateTurma;

public class AtualizarTurmaCommand : IRequest<TurmaResponse>
{
    public int Id { get; }
    public TurmaRequest Dto { get; }
    public AtualizarTurmaCommand(int id, TurmaRequest dto)
    {
        Id = id;
        Dto = dto;
    }
}

using MediatR;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;

namespace Portal_Aluno.Application.Features.TurmaFeature.Queries.GetTurmaById;

public class GetTurmaByIdQuery : IRequest<TurmaResponse>
{
    public int Id { get; }
    public GetTurmaByIdQuery(int id) => Id = id;
}

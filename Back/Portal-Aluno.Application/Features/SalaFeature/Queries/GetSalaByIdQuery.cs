using MediatR;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;

namespace Portal_Aluno.Application.Features.SalaFeature.Queries;

public class GetSalaByIdQuery : IRequest<SalaResponse>
{
    public int Id { get; }

    public GetSalaByIdQuery(int id)
    {
        Id = id;
    }
}

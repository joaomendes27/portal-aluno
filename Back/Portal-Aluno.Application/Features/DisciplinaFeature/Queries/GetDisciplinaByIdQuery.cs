using MediatR;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;

namespace Portal_Aluno.Application.Features.DisciplinaFeature.Queries;

public class GetDisciplinaByIdQuery : IRequest<DisciplinaResponse>
{
    public int Id { get; set; }
    public GetDisciplinaByIdQuery(int id)
    {
        Id = id;
    }
}

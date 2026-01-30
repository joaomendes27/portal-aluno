using MediatR;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;

namespace Portal_Aluno.Application.Features.DisciplinaFeature.Queries;

public class GetAllDisciplinasQuery : IRequest<List<DisciplinaResponse>>
{
}

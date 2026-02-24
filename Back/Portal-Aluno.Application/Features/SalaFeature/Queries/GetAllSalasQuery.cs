using MediatR;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;

namespace Portal_Aluno.Application.Features.SalaFeature.Queries;

public class GetAllSalasQuery : IRequest<List<SalaResponse>>
{
}

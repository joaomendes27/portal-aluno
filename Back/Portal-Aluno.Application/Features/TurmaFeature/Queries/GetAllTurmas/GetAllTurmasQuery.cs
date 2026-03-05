using MediatR;
using System.Collections.Generic;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;

namespace Portal_Aluno.Application.Features.TurmaFeature.Queries.GetAllTurmas;

public class GetAllTurmasQuery : IRequest<List<TurmaResponse>>
{
}

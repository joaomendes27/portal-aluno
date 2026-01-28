using MediatR;
using Portal_Aluno.Application.Features.AgendaFeature.DTOs;

namespace Portal_Aluno.Application.Features.AgendaFeature.Queries.Professor;

public class GetAgendaHojeProfessorQuery : IRequest<List<AgendaHojeResponse>>
{
    public int ProfessorId { get; }
    public GetAgendaHojeProfessorQuery(int professorId) => ProfessorId = professorId;
}

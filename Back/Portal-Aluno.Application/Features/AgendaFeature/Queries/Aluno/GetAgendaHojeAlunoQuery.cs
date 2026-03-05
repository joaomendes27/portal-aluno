using MediatR;
using Portal_Aluno.Application.Features.AgendaFeature.DTOs;

namespace Portal_Aluno.Application.Features.AgendaFeature.Queries.Aluno;

public class GetAgendaHojeAlunoQuery : IRequest<List<AgendaHojeResponse>>
{
    public int AlunoId { get; }
    public GetAgendaHojeAlunoQuery(int alunoId) => AlunoId = alunoId;
}

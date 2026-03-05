using MediatR;
using Portal_Aluno.Application.Features.AgendaFeature.DTOs;
using Portal_Aluno.Domain.Interfaces;
using System.Globalization;

namespace Portal_Aluno.Application.Features.AgendaFeature.Queries.Professor;

public class GetAgendaHojeProfessorQueryHandler : IRequestHandler<GetAgendaHojeProfessorQuery, List<AgendaHojeResponse>>
{
    private readonly ITurmaRepository _turmaRepository;

    public GetAgendaHojeProfessorQueryHandler(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;
    }

    public async Task<List<AgendaHojeResponse>> Handle(GetAgendaHojeProfessorQuery request, CancellationToken cancellationToken)
    {
        var turmas = (await _turmaRepository.GetAllAsync())
            .Where(t => t.ProfessorId == request.ProfessorId)
            .ToList();
        var hoje = DateTime.Now;
        var diaSemana = hoje.ToString("dddd", new CultureInfo("pt-BR"));

        var result = turmas
            .Where(t => string.Equals(t.DiaSemana, diaSemana, StringComparison.OrdinalIgnoreCase))
            .Select(t => new AgendaHojeResponse(
                t.Disciplina?.Nome ?? "",
                t.Professor?.Nome ?? "",
                t.Curso?.Nome ?? "",
                t.Sala?.Numero ?? "",
                t.Sala?.Andar,
                t.DiaSemana,
                t.HoraAulaInicio?.ToString("HH:mm"),
                t.HoraAulaFim?.ToString("HH:mm")
            ))
            .ToList();

        return result;
    }
}

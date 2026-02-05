using MediatR;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;

namespace Portal_Aluno.Application.Features.TurmaFeature.Queries.GetAllTurmas;

public class GetAllTurmasQueryHandler : IRequestHandler<GetAllTurmasQuery, List<TurmaResponse>>
{
    private readonly ITurmaRepository _turmaRepository;

    public GetAllTurmasQueryHandler(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;
    }

    public async Task<List<TurmaResponse>> Handle(GetAllTurmasQuery request, CancellationToken cancellationToken)
    {
        var turmas = await _turmaRepository.GetAllAsync();
        var responses = turmas.Select(t => new TurmaResponse(
            t.Id,
            t.CursoId,
            t.Curso?.Nome ?? "",
            t.DisciplinaId,
            t.Disciplina?.Nome ?? "",
            t.Semestre,
            t.Ano,
            t.ProfessorId,
            t.Professor?.Nome ?? "",
            t.Status,
            t.Capacidade,
            t.DiaSemana,
            t.HoraAulaInicio?.ToString("HH:mm"),
            t.HoraAulaFim?.ToString("HH:mm"),
            t.SalaId,
            t.Sala?.Numero ?? ""
        )).ToList();
        return responses;
    }
}

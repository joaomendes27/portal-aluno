using MediatR;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;

namespace Portal_Aluno.Application.Features.TurmaFeature.Queries.GetTurmaById;

public class GetTurmaByIdQueryHandler : IRequestHandler<GetTurmaByIdQuery, TurmaResponse>
{
    private readonly ITurmaRepository _turmaRepository;

    public GetTurmaByIdQueryHandler(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;
    }

    public async Task<TurmaResponse> Handle(GetTurmaByIdQuery request, CancellationToken cancellationToken)
    {
        var t = await _turmaRepository.GetByIdAsync(request.Id);
        if (t == null)
            throw new Exception("Turma não encontrada.");
        return new TurmaResponse(
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
        );
    }
}

using MediatR;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.DisciplinaFeature.Queries;

public class GetDisciplinaByIdQueryHandler : IRequestHandler<GetDisciplinaByIdQuery, DisciplinaResponse>
{
    private readonly IDisciplinaRepository _disciplinaRepository;

    public GetDisciplinaByIdQueryHandler(IDisciplinaRepository disciplinaRepository)
    {
        _disciplinaRepository = disciplinaRepository;
    }

    public async Task<DisciplinaResponse> Handle(GetDisciplinaByIdQuery request, CancellationToken cancellationToken)
    {
        var disciplina = await _disciplinaRepository.GetByIdAsync(request.Id);
        if (disciplina == null) return null!;
        var cursoDisciplina = disciplina.CursoDisciplinas.FirstOrDefault();
        return new DisciplinaResponse
        {
            Id = disciplina.Id,
            CursoId = cursoDisciplina?.CursoId ?? 0,
            Nome = disciplina.Nome ?? string.Empty,
            CargaHoraria = disciplina.CargaHoraria ?? 0,
            LimiteFaltas = disciplina.LimiteFaltas ?? 0,
            Semestre = cursoDisciplina?.Semestre ?? 0
        };
    }
}

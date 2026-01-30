using MediatR;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.DisciplinaFeature.Queries;

public class GetAllDisciplinasQueryHandler : IRequestHandler<GetAllDisciplinasQuery, List<DisciplinaResponse>>
{
    private readonly IDisciplinaRepository _disciplinaRepository;

    public GetAllDisciplinasQueryHandler(IDisciplinaRepository disciplinaRepository)
    {
        _disciplinaRepository = disciplinaRepository;
    }

    public async Task<List<DisciplinaResponse>> Handle(GetAllDisciplinasQuery request, CancellationToken cancellationToken)
    {
        var disciplinas = await _disciplinaRepository.GetAllAsync();
        var result = disciplinas.Select(d => {
            var cursoDisciplina = d.CursoDisciplinas.FirstOrDefault();
            return new DisciplinaResponse
            {
                Id = d.Id,
                CursoId = cursoDisciplina?.CursoId ?? 0,
                Nome = d.Nome ?? string.Empty,
                CargaHoraria = d.CargaHoraria ?? 0,
                LimiteFaltas = d.LimiteFaltas ?? 0,
                Semestre = cursoDisciplina?.Semestre ?? 0
            };
        }).ToList();
        return result;
    }
}

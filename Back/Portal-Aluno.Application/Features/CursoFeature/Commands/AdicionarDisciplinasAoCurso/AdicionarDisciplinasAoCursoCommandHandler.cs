using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.CursoFeature.Commands.AdicionarDisciplinasAoCurso;

public class AdicionarDisciplinasAoCursoCommandHandler : IRequestHandler<AdicionarDisciplinasAoCursoCommand, Unit>
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly ICursoDisciplinaRepository _cursoDisciplinaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdicionarDisciplinasAoCursoCommandHandler(
        ICursoRepository cursoRepository,
        IDisciplinaRepository disciplinaRepository,
        ICursoDisciplinaRepository cursoDisciplinaRepository,
        IUnitOfWork unitOfWork)
    {
        _cursoRepository = cursoRepository;
        _disciplinaRepository = disciplinaRepository;
        _cursoDisciplinaRepository = cursoDisciplinaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AdicionarDisciplinasAoCursoCommand request, CancellationToken cancellationToken)
    {
        var curso = await _cursoRepository.GetByIdAsync(request.Dto.CursoId);
        if (curso == null)
            throw new KeyNotFoundException($"Curso com ID {request.Dto.CursoId} não encontrado.");

        var novosCursoDisciplinas = new List<CursoDisciplina>();

        foreach (var item in request.Dto.Disciplinas)
        {
            var disciplina = await _disciplinaRepository.GetByIdAsync(item.DisciplinaId);
            if (disciplina == null)
                throw new KeyNotFoundException($"Disciplina com ID {item.DisciplinaId} não encontrada.");

            // Verifica se já existe o vínculo
            var vinculoExistente = await _cursoDisciplinaRepository.GetByCursoAndDisciplinaAsync(request.Dto.CursoId, item.DisciplinaId);
            if (vinculoExistente != null)
                continue; // Ignora se já existe

            novosCursoDisciplinas.Add(new CursoDisciplina
            {
                CursoId = request.Dto.CursoId,
                DisciplinaId = item.DisciplinaId,
                Semestre = item.Semestre
            });
        }

        if (novosCursoDisciplinas.Any())
        {
            await _cursoDisciplinaRepository.AddRangeAsync(novosCursoDisciplinas);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}

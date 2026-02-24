using MediatR;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;

namespace Portal_Aluno.Application.Features.TurmaFeature.Commands.UpdateTurma;

public class AtualizarTurmaCommandHandler : IRequestHandler<AtualizarTurmaCommand, TurmaResponse>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISalaRepository _salaRepository;
    private readonly ICursoRepository _cursoRepository;
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly IProfessorRepository _professorRepository;

    public AtualizarTurmaCommandHandler(ITurmaRepository turmaRepository, IUnitOfWork unitOfWork, ISalaRepository salaRepository, ICursoRepository cursoRepository, IDisciplinaRepository disciplinaRepository, IProfessorRepository professorRepository)
    {
        _turmaRepository = turmaRepository;
        _unitOfWork = unitOfWork;
        _salaRepository = salaRepository;
        _cursoRepository = cursoRepository;
        _disciplinaRepository = disciplinaRepository;
        _professorRepository = professorRepository;
    }

    public async Task<TurmaResponse> Handle(AtualizarTurmaCommand request, CancellationToken cancellationToken)
    {
        var turma = await _turmaRepository.GetByIdAsync(request.Id);
        if (turma == null)
            throw new Exception("Turma não encontrada.");

        turma.CursoId = request.Dto.CursoId;
        turma.DisciplinaId = request.Dto.DisciplinaId;
        turma.Semestre = request.Dto.Semestre;
        turma.Ano = request.Dto.Ano;
        turma.Turno = request.Dto.Turno;
        turma.ProfessorId = request.Dto.ProfessorId;
        turma.Status = request.Dto.Status;
        turma.Capacidade = request.Dto.Capacidade;
        turma.DiaSemana = request.Dto.DiaSemana;
        turma.HoraAulaInicio = request.Dto.HoraInicio != null ? TimeOnly.Parse(request.Dto.HoraInicio) : null;
        turma.HoraAulaFim = request.Dto.HoraFim != null ? TimeOnly.Parse(request.Dto.HoraFim) : null;
        turma.SalaId = request.Dto.SalaId;

        _turmaRepository.Update(turma);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var curso = await _cursoRepository.GetByIdAsync(turma.CursoId);
        var disciplina = await _disciplinaRepository.GetByIdAsync(turma.DisciplinaId);
        var professor = await _professorRepository.GetByIdAsync(turma.ProfessorId);
        var sala = turma.SalaId.HasValue ? await _salaRepository.GetByIdAsync(turma.SalaId.Value) : null;

        return new TurmaResponse(
            turma.Id,
            turma.CursoId,
            curso?.Nome ?? "N/A",
            turma.DisciplinaId,
            disciplina?.Nome ?? "N/A",
            turma.Semestre,
            turma.Ano,
            turma.Turno,
            turma.ProfessorId,
            professor?.Nome ?? "N/A",
            turma.Status,
            turma.Capacidade,
            turma.DiaSemana,
            turma.HoraAulaInicio?.ToString("HH:mm"),
            turma.HoraAulaFim?.ToString("HH:mm"),
            turma.SalaId,
            sala?.Numero ?? "N/A"
        );
    }
}

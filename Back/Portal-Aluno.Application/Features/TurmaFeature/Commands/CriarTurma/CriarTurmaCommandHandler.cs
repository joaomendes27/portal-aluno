using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;
using System.Data;

namespace Portal_Aluno.Application.Features.TurmaFeature.Commands.CreateTurma;

public class CriarTurmaCommandHandler : IRequestHandler<CriarTurmaCommand, TurmaResponse>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISalaRepository _salaRepository;
    private readonly ICursoRepository _cursoRepository;
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly IProfessorRepository _professorRepository;

    public CriarTurmaCommandHandler(ITurmaRepository turmaRepository, IUnitOfWork unitOfWork, ISalaRepository salaRepository, ICursoRepository cursoRepository, IDisciplinaRepository disciplinaRepository, IProfessorRepository professorRepository)
    {
        _turmaRepository = turmaRepository;
        _unitOfWork = unitOfWork;
        _salaRepository = salaRepository;
        _cursoRepository = cursoRepository;
        _disciplinaRepository = disciplinaRepository;
        _professorRepository = professorRepository;
    }

    public async Task<TurmaResponse> Handle(CriarTurmaCommand request, CancellationToken cancellationToken)
    {
        var turma = new Turma
        {
            CursoId = request.Dto.CursoId,
            DisciplinaId = request.Dto.DisciplinaId,
            Semestre = request.Dto.Semestre,
            Ano = request.Dto.Ano,
            ProfessorId = request.Dto.ProfessorId,
            Status = request.Dto.Status,
            Capacidade = request.Dto.Capacidade,
            DiaSemana = request.Dto.DiaSemana,
            HoraAulaInicio = request.Dto.HoraInicio != null ? TimeOnly.Parse(request.Dto.HoraInicio) : null,
            HoraAulaFim = request.Dto.HoraFim != null ? TimeOnly.Parse(request.Dto.HoraFim) : null,
            SalaId = request.Dto.SalaId
        };

        await _turmaRepository.AddAsync(turma);
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

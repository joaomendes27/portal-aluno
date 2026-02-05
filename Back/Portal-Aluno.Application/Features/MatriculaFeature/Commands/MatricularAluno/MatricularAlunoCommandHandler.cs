using MediatR;
using Portal_Aluno.Application.Features.MatriculaFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.MatriculaFeature.Commands.MatricularAluno;

public class MatricularAlunoCommandHandler : IRequestHandler<MatricularAlunoCommand, MatricularAlunoResponse>
{
    private readonly IMatriculaRepository _matriculaRepository;
    private readonly IMatriculaTurmaRepository _matriculaTurmaRepository;
    private readonly IAlunoRepository _alunoRepository;
    private readonly ICursoRepository _cursoRepository;
    private readonly ITurmaRepository _turmaRepository;
    private readonly ICursoDisciplinaRepository _cursoDisciplinaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MatricularAlunoCommandHandler(
        IMatriculaRepository matriculaRepository,
        IMatriculaTurmaRepository matriculaTurmaRepository,
        IAlunoRepository alunoRepository,
        ICursoRepository cursoRepository,
        ITurmaRepository turmaRepository,
        ICursoDisciplinaRepository cursoDisciplinaRepository,
        IUnitOfWork unitOfWork)
    {
        _matriculaRepository = matriculaRepository;
        _matriculaTurmaRepository = matriculaTurmaRepository;
        _alunoRepository = alunoRepository;
        _cursoRepository = cursoRepository;
        _turmaRepository = turmaRepository;
        _cursoDisciplinaRepository = cursoDisciplinaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MatricularAlunoResponse> Handle(MatricularAlunoCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        // Validar aluno
        var aluno = await _alunoRepository.GetByIdAsync(dto.AlunoRa);
        if (aluno == null)
            throw new KeyNotFoundException($"Aluno com RA {dto.AlunoRa} não encontrado.");

        // Validar curso
        var curso = await _cursoRepository.GetByIdAsync(dto.CursoId);
        if (curso == null)
            throw new KeyNotFoundException($"Curso com ID {dto.CursoId} não encontrado.");

        // Verificar se já tem matrícula ativa
        var matriculaExistente = await _matriculaRepository.GetByAlunoRaAsync(dto.AlunoRa);
        if (matriculaExistente != null && matriculaExistente.Status == "Ativa")
            throw new InvalidOperationException($"Aluno com RA {dto.AlunoRa} já possui matrícula ativa.");

        // Buscar disciplinas do curso para o semestre
        var disciplinasDoCurso = await _cursoDisciplinaRepository.GetByCursoIdAsync(dto.CursoId);
        var disciplinasDoSemestre = disciplinasDoCurso
            .Where(cd => cd.Semestre == dto.Semestre)
            .ToList();

        if (!disciplinasDoSemestre.Any())
            throw new InvalidOperationException($"Não há disciplinas cadastradas para o semestre {dto.Semestre} do curso.");

        // Buscar turmas disponíveis
        var turmasDisponiveis = await _turmaRepository.GetTurmasDisponiveisAsync(
            dto.CursoId, dto.Semestre, dto.Turno, dto.Ano);

        // Criar a matrícula
        var matricula = new Matricula
        {
            Ra = dto.AlunoRa,
            CursoId = dto.CursoId,
            Semestre = dto.Semestre,
            Turno = dto.Turno,
            DataMatricula = DateTime.UtcNow,
            Status = "Ativa",
            FormaIngresso = dto.FormaIngresso
        };

        await _matriculaRepository.AddAsync(matricula);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Inscrever o aluno nas turmas
        var turmasInscritas = new List<TurmaInscritaResponse>();
        var avisos = new List<string>();

        foreach (var disciplinaCurso in disciplinasDoSemestre)
        {
            // Buscar turmas dessa disciplina
            var turmasDaDisciplina = turmasDisponiveis
                .Where(t => t.DisciplinaId == disciplinaCurso.DisciplinaId)
                .OrderBy(t => t.Id) // Ordenar para pegar turma A, B, C...
                .ToList();

            if (!turmasDaDisciplina.Any())
            {
                avisos.Add($"Não há turmas disponíveis para a disciplina '{disciplinaCurso.Disciplina?.Nome}' no período {dto.Ano}.");
                continue;
            }

            // Tentar encontrar uma turma com vaga
            Turma? turmaComVaga = null;
            foreach (var turma in turmasDaDisciplina)
            {
                var alunosNaTurma = turma.MatriculaTurmas?.Count ?? 0;
                if (turma.Capacidade == null || alunosNaTurma < turma.Capacidade)
                {
                    turmaComVaga = turma;
                    break;
                }
            }

            if (turmaComVaga == null)
            {
                avisos.Add($"Todas as turmas da disciplina '{disciplinaCurso.Disciplina?.Nome}' estão lotadas.");
                continue;
            }

            // Inscrever na turma
            var matriculaTurma = new MatriculaTurma
            {
                MatriculaId = matricula.Id,
                TurmaId = turmaComVaga.Id,
                Situacao = "Cursando"
            };

            await _matriculaTurmaRepository.AddAsync(matriculaTurma);

            turmasInscritas.Add(new TurmaInscritaResponse
            {
                TurmaId = turmaComVaga.Id,
                DisciplinaNome = turmaComVaga.Disciplina?.Nome ?? "",
                ProfessorNome = turmaComVaga.Professor?.Nome ?? "",
                DiaSemana = turmaComVaga.DiaSemana,
                HoraInicio = turmaComVaga.HoraAulaInicio?.ToString("HH:mm"),
                HoraFim = turmaComVaga.HoraAulaFim?.ToString("HH:mm"),
                Sala = turmaComVaga.Sala?.Numero
            });
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MatricularAlunoResponse
        {
            MatriculaId = matricula.Id,
            AlunoRa = dto.AlunoRa,
            CursoNome = curso.Nome ?? "",
            Semestre = dto.Semestre,
            Turno = dto.Turno,
            TurmasInscritas = turmasInscritas,
            Avisos = avisos
        };
    }
}

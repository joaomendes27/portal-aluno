using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.MatriculaFeature.Commands.InscreverEmTurma;

public class InscreverEmTurmaCommandHandler : IRequestHandler<InscreverEmTurmaCommand, int>
{
    private readonly IMatriculaTurmaRepository _matriculaTurmaRepository;
    private readonly IMatriculaRepository _matriculaRepository;
    private readonly ITurmaRepository _turmaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InscreverEmTurmaCommandHandler(
        IMatriculaTurmaRepository matriculaTurmaRepository,
        IMatriculaRepository matriculaRepository,
        ITurmaRepository turmaRepository,
        IUnitOfWork unitOfWork)
    {
        _matriculaTurmaRepository = matriculaTurmaRepository;
        _matriculaRepository = matriculaRepository;
        _turmaRepository = turmaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(InscreverEmTurmaCommand request, CancellationToken cancellationToken)
    {
        var matricula = await _matriculaRepository.GetByIdAsync(request.Dto.MatriculaId);
        if (matricula == null)
            throw new KeyNotFoundException($"Matrícula com ID {request.Dto.MatriculaId} não encontrada.");

        var turma = await _turmaRepository.GetByIdAsync(request.Dto.TurmaId);
        if (turma == null)
            throw new KeyNotFoundException($"Turma com ID {request.Dto.TurmaId} não encontrada.");

        // Verifica se a turma é do mesmo curso da matrícula
        if (turma.CursoId != matricula.CursoId)
            throw new InvalidOperationException("A turma não pertence ao curso do aluno.");

        // Verifica se já está inscrito
        var inscricaoExistente = await _matriculaTurmaRepository.GetByMatriculaAndTurmaAsync(request.Dto.MatriculaId, request.Dto.TurmaId);
        if (inscricaoExistente != null)
            throw new InvalidOperationException("Aluno já está inscrito nesta turma.");

        var matriculaTurma = new MatriculaTurma
        {
            MatriculaId = request.Dto.MatriculaId,
            TurmaId = request.Dto.TurmaId,
            Situacao = "Cursando"
        };

        await _matriculaTurmaRepository.AddAsync(matriculaTurma);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return matriculaTurma.Id;
    }
}

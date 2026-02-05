using MediatR;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.TurmaFeature.Commands.DeleteTurma;

public class DeletarTurmaCommandHandler : IRequestHandler<DeletarTurmaCommand, Unit>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletarTurmaCommandHandler(ITurmaRepository turmaRepository, IUnitOfWork unitOfWork)
    {
        _turmaRepository = turmaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeletarTurmaCommand request, CancellationToken cancellationToken)
    {
        var turma = await _turmaRepository.GetByIdAsync(request.Id);
        if (turma != null)
        {
            _turmaRepository.Delete(turma);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        return Unit.Value;
    }
}
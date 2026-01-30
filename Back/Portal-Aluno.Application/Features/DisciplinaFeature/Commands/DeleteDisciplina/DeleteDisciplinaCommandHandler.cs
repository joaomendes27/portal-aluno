using MediatR;
using Portal_Aluno.Domain.Interfaces;

namespace Features.DisciplinaFeature.Commands.DeleteDisciplina;

public class DeleteDisciplinaCommandHandler : IRequestHandler<DeleteDisciplinaCommand, Unit>
{
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDisciplinaCommandHandler(IDisciplinaRepository disciplinaRepository, IUnitOfWork unitOfWork)
    {
        _disciplinaRepository = disciplinaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteDisciplinaCommand request, CancellationToken cancellationToken)
    {
        await _disciplinaRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
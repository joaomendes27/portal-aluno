using MediatR;
using Portal_Aluno.Domain.Interfaces;

namespace Features.DisciplinaFeature.Commands.DeleteDisciplina;

public class DeletarDisciplinaCommandHandler : IRequestHandler<DeletarDisciplinaCommand, Unit>
{
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly ICursoDisciplinaRepository _cursoDisciplinaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletarDisciplinaCommandHandler(
        IDisciplinaRepository disciplinaRepository,
        ICursoDisciplinaRepository cursoDisciplinaRepository,
        IUnitOfWork unitOfWork)
    {
        _disciplinaRepository = disciplinaRepository;
        _cursoDisciplinaRepository = cursoDisciplinaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeletarDisciplinaCommand request, CancellationToken cancellationToken)
    {
        var vinculos = await _cursoDisciplinaRepository.GetByDisciplinaIdAsync(request.Id);
        if (vinculos.Any())
        {
            _cursoDisciplinaRepository.DeleteRange(vinculos);
        }

        await _disciplinaRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
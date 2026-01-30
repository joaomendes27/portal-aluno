using MediatR;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.DisciplinaFeature.Commands;

public class UpdateDisciplinaCommandHandler : IRequestHandler<UpdateDisciplinaCommand, Unit>
{
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDisciplinaCommandHandler(IDisciplinaRepository disciplinaRepository, IUnitOfWork unitOfWork)
    {
        _disciplinaRepository = disciplinaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateDisciplinaCommand request, CancellationToken cancellationToken)
    {
        var disciplina = await _disciplinaRepository.GetByIdAsync(request.Id);
        if (disciplina == null)
            throw new Exception("Disciplina não encontrada.");

        disciplina.Nome = request.Request.Nome;
        disciplina.CargaHoraria = request.Request.CargaHoraria;
        disciplina.LimiteFaltas = request.Request.LimiteFaltas;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
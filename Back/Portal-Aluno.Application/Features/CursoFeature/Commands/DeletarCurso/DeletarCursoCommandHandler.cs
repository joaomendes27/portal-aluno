using MediatR;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.CursoFeature.Commands.DeletarCurso;

public class DeletarCursoCommandHandler : IRequestHandler<DeletarCursoCommand, Unit>
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletarCursoCommandHandler(ICursoRepository cursoRepository, IUnitOfWork unitOfWork)
    {
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeletarCursoCommand request, CancellationToken cancellationToken)
    {
        var curso = await _cursoRepository.GetByIdAsync(request.Id);

        if (curso == null)
            throw new KeyNotFoundException($"Curso com ID {request.Id} não encontrado.");

        _cursoRepository.Delete(curso);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}

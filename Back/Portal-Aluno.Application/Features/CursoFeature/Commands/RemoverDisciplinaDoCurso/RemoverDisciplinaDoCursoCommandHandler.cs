using MediatR;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.CursoFeature.Commands.RemoverDisciplinaDoCurso;

public class RemoverDisciplinaDoCursoCommandHandler : IRequestHandler<RemoverDisciplinaDoCursoCommand, Unit>
{
    private readonly ICursoDisciplinaRepository _cursoDisciplinaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoverDisciplinaDoCursoCommandHandler(
        ICursoDisciplinaRepository cursoDisciplinaRepository,
        IUnitOfWork unitOfWork)
    {
        _cursoDisciplinaRepository = cursoDisciplinaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoverDisciplinaDoCursoCommand request, CancellationToken cancellationToken)
    {
        var vinculo = await _cursoDisciplinaRepository.GetByCursoAndDisciplinaAsync(
            request.Dto.CursoId, 
            request.Dto.DisciplinaId);

        if (vinculo == null)
            throw new KeyNotFoundException($"Vínculo entre curso {request.Dto.CursoId} e disciplina {request.Dto.DisciplinaId} não encontrado.");

        _cursoDisciplinaRepository.Delete(vinculo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

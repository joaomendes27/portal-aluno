using MediatR;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.CursoFeature.Commands.AtualizarCurso;

public class AtualizarCursoCommandHandler : IRequestHandler<AtualizarCursoCommand, Unit>
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AtualizarCursoCommandHandler(ICursoRepository cursoRepository, IUnitOfWork unitOfWork)
    {
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AtualizarCursoCommand request, CancellationToken cancellationToken)
    {
        var curso = await _cursoRepository.GetByIdAsync(request.Id);

        if (curso == null)
            throw new KeyNotFoundException($"Curso com ID {request.Id} não encontrado.");

        curso.Nome = request.Dto.Nome;
        curso.Grau = request.Dto.Grau;
        curso.CargaHoraria = request.Dto.CargaHoraria;

        _cursoRepository.Update(curso);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}

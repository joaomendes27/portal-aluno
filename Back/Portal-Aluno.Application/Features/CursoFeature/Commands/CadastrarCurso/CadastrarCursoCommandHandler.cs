using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.CursoFeature.Commands.CadastrarCurso;

public class CadastrarCursoCommandHandler : IRequestHandler<CadastrarCursoCommand, int>
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CadastrarCursoCommandHandler(ICursoRepository cursoRepository, IUnitOfWork unitOfWork)
    {
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CadastrarCursoCommand request, CancellationToken cancellationToken)
    {
        var curso = new Curso
        {
            Nome = request.Dto.Nome,
            Grau = request.Dto.Grau,
            CargaHoraria = request.Dto.CargaHoraria
        };

        await _cursoRepository.AddAsync(curso);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return curso.Id;
    }
}

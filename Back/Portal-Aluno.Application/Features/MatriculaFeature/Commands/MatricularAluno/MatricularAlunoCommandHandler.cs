using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.MatriculaFeature.Commands.MatricularAluno;

public class MatricularAlunoCommandHandler : IRequestHandler<MatricularAlunoCommand, int>
{
    private readonly IMatriculaRepository _matriculaRepository;
    private readonly IAlunoRepository _alunoRepository;
    private readonly ICursoRepository _cursoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MatricularAlunoCommandHandler(
        IMatriculaRepository matriculaRepository,
        IAlunoRepository alunoRepository,
        ICursoRepository cursoRepository,
        IUnitOfWork unitOfWork)
    {
        _matriculaRepository = matriculaRepository;
        _alunoRepository = alunoRepository;
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(MatricularAlunoCommand request, CancellationToken cancellationToken)
    {
        var aluno = await _alunoRepository.GetByIdAsync(request.Dto.AlunoRa);
        if (aluno == null)
            throw new KeyNotFoundException($"Aluno com RA {request.Dto.AlunoRa} não encontrado.");

        var curso = await _cursoRepository.GetByIdAsync(request.Dto.CursoId);
        if (curso == null)
            throw new KeyNotFoundException($"Curso com ID {request.Dto.CursoId} não encontrado.");

        var matriculaExistente = await _matriculaRepository.GetByAlunoRaAsync(request.Dto.AlunoRa);
        if (matriculaExistente != null)
            throw new InvalidOperationException($"Aluno com RA {request.Dto.AlunoRa} já possui matrícula ativa.");

        var matricula = new Matricula
        {
            Ra = request.Dto.AlunoRa,
            CursoId = request.Dto.CursoId,
            Semestre = request.Dto.Semestre,
            Turno = request.Dto.Turno,
            DataMatricula = DateTime.UtcNow,
            Status = "Ativa",
            FormaIngresso = request.Dto.FormaIngresso
        };

        await _matriculaRepository.AddAsync(matricula);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return matricula.Id;
    }
}

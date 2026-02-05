using MediatR;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Features.DisciplinaFeature.Commands.CreateDisciplina;

public class CriarDisciplinaCommandHandler : IRequestHandler<CriarDisciplinaCommand, int>
{
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly ICursoRepository _cursoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CriarDisciplinaCommandHandler(IDisciplinaRepository disciplinaRepository, ICursoRepository cursoRepository, IUnitOfWork unitOfWork)
    {
        _disciplinaRepository = disciplinaRepository;
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CriarDisciplinaCommand request, CancellationToken cancellationToken)
    {
        var curso = await _cursoRepository.GetByIdAsync(request.Request.CursoId);
        if (curso == null)
            throw new Exception("Curso não encontrado.");

        var disciplina = new Disciplina
        {
            Nome = request.Request.Nome,
            CargaHoraria = request.Request.CargaHoraria,
            LimiteFaltas = request.Request.LimiteFaltas
        };

        var cursoDisciplina = new CursoDisciplina
        {
            Curso = curso,
            Disciplina = disciplina,
            Semestre = request.Request.Semestre
        };

        disciplina.CursoDisciplinas.Add(cursoDisciplina);
        await _disciplinaRepository.AddAsync(disciplina);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return disciplina.Id;
    }
}

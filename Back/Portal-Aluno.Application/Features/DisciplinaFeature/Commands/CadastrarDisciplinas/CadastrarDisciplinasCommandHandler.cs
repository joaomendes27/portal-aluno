using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.DisciplinaFeature.Commands.CadastrarDisciplinas;

public class CadastrarDisciplinasCommandHandler : IRequestHandler<CadastrarDisciplinasCommand, List<int>>
{
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly ICursoRepository _cursoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CadastrarDisciplinasCommandHandler(
        IDisciplinaRepository disciplinaRepository,
        ICursoRepository cursoRepository,
        IUnitOfWork unitOfWork)
    {
        _disciplinaRepository = disciplinaRepository;
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<int>> Handle(CadastrarDisciplinasCommand request, CancellationToken cancellationToken)
    {
        var curso = await _cursoRepository.GetByIdAsync(request.Request.CursoId);
        if (curso == null)
            throw new KeyNotFoundException($"Curso com ID {request.Request.CursoId} não encontrado.");

        var disciplinas = new List<Disciplina>();

        foreach (var item in request.Request.Disciplinas)
        {
            var disciplina = new Disciplina
            {
                Nome = item.Nome,
                CargaHoraria = item.CargaHoraria,
                LimiteFaltas = item.LimiteFaltas
            };

            var cursoDisciplina = new CursoDisciplina
            {
                CursoId = request.Request.CursoId,
                Disciplina = disciplina,
                Semestre = item.Semestre
            };

            disciplina.CursoDisciplinas.Add(cursoDisciplina);
            await _disciplinaRepository.AddAsync(disciplina);
            disciplinas.Add(disciplina);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return disciplinas.Select(d => d.Id).ToList();
    }
}

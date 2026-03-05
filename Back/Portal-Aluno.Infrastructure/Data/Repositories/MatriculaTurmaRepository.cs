using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Infrastructure.Data.Repositories;

public class MatriculaTurmaRepository : IMatriculaTurmaRepository
{
    private readonly AppDbContext _context;

    public MatriculaTurmaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MatriculaTurma?> GetByIdAsync(int id)
    {
        return await _context.MatriculasTurma
            .Include(mt => mt.Matricula)
            .Include(mt => mt.Turma)
            .FirstOrDefaultAsync(mt => mt.Id == id);
    }

    public async Task<MatriculaTurma?> GetByMatriculaAndTurmaAsync(int matriculaId, int turmaId)
    {
        return await _context.MatriculasTurma
            .FirstOrDefaultAsync(mt => mt.MatriculaId == matriculaId && mt.TurmaId == turmaId);
    }

    public async Task<List<MatriculaTurma>> GetByMatriculaIdAsync(int matriculaId)
    {
        return await _context.MatriculasTurma
            .Include(mt => mt.Turma)
                .ThenInclude(t => t.Disciplina)
            .Include(mt => mt.Turma)
                .ThenInclude(t => t.Professor)
            .Where(mt => mt.MatriculaId == matriculaId)
            .ToListAsync();
    }

    public async Task<List<MatriculaTurma>> GetByTurmaIdAsync(int turmaId)
    {
        return await _context.MatriculasTurma
            .Include(mt => mt.Matricula)
                .ThenInclude(m => m.Aluno)
            .Where(mt => mt.TurmaId == turmaId)
            .ToListAsync();
    }

    public async Task AddAsync(MatriculaTurma matriculaTurma)
    {
        await _context.MatriculasTurma.AddAsync(matriculaTurma);
    }

    public void Update(MatriculaTurma matriculaTurma)
    {
        _context.MatriculasTurma.Update(matriculaTurma);
    }

    public void Delete(MatriculaTurma matriculaTurma)
    {
        _context.MatriculasTurma.Remove(matriculaTurma);
    }
}

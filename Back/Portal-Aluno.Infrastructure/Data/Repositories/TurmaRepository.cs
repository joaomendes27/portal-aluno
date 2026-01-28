using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Infrastructure.Data.Repositories;

public class TurmaRepository : ITurmaRepository
{
    private readonly AppDbContext _context;

    public TurmaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Turma?> GetByIdAsync(int id)
    {
        return await _context.Turmas
            .Include(t => t.Curso)
            .Include(t => t.Disciplina)
            .Include(t => t.Professor)
            .Include(t => t.Sala)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Turma>> GetAllAsync()
    {
        return await _context.Turmas
            .Include(t => t.Curso)
            .Include(t => t.Disciplina)
            .Include(t => t.Professor)
            .Include(t => t.Sala)
            .ToListAsync();
    }

    public async Task AddAsync(Turma turma)
    {
        await _context.Turmas.AddAsync(turma);
    }

    public void Update(Turma turma)
    {
        _context.Turmas.Update(turma);
    }

    public void Delete(Turma turma)
    {
        _context.Turmas.Remove(turma);
    }
}

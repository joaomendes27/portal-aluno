using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Infrastructure.Data.Repositories;

public class DisciplinaRepository : IDisciplinaRepository
{
    private readonly AppDbContext _context;

    public DisciplinaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Disciplina?> GetByIdAsync(int id)
    {
        return await _context.Disciplinas
            .Include(d => d.CursoDisciplinas)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<List<Disciplina>> GetAllAsync()
    {
        return await _context.Disciplinas
            .Include(d => d.CursoDisciplinas)
            .ToListAsync();
    }

    public async Task AddAsync(Disciplina disciplina)
    {
        await _context.Disciplinas.AddAsync(disciplina);
    }

    public async Task DeleteAsync(int id)
    {
        var disciplina = await _context.Disciplinas.FindAsync(id);
        if (disciplina != null)
        {
            _context.Disciplinas.Remove(disciplina);
        }
    }
}

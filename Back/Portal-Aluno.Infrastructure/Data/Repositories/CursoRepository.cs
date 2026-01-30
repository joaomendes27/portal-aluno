using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Infrastructure.Data.Repositories;

public class CursoRepository : ICursoRepository
{
    private readonly AppDbContext _context;

    public CursoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Curso curso)
    {
        await _context.Cursos.AddAsync(curso);
    }

    public async Task<List<Curso>> GetAllAsync()
    {
        return await _context.Cursos.ToListAsync();
    }

    public async Task<Curso?> GetByIdAsync(int id)
    {
        return await _context.Cursos.FindAsync(id);
    }

    public async Task<Curso?> GetByIdWithDisciplinasAsync(int id)
    {
        return await _context.Cursos
            .Include(c => c.CursoDisciplinas)
                .ThenInclude(cd => cd.Disciplina)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}

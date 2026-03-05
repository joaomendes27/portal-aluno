using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Infrastructure.Data.Repositories;

public class CursoDisciplinaRepository : ICursoDisciplinaRepository
{
    private readonly AppDbContext _context;

    public CursoDisciplinaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CursoDisciplina>> GetByCursoIdAsync(int cursoId)
    {
        return await _context.CursosDisciplinas
            .Include(cd => cd.Disciplina)
            .Where(cd => cd.CursoId == cursoId)
            .ToListAsync();
    }

    public async Task<List<CursoDisciplina>> GetByDisciplinaIdAsync(int disciplinaId)
    {
        return await _context.CursosDisciplinas
            .Include(cd => cd.Curso)
            .Where(cd => cd.DisciplinaId == disciplinaId)
            .ToListAsync();
    }

    public async Task<CursoDisciplina?> GetByCursoAndDisciplinaAsync(int cursoId, int disciplinaId)
    {
        return await _context.CursosDisciplinas
            .FirstOrDefaultAsync(cd => cd.CursoId == cursoId && cd.DisciplinaId == disciplinaId);
    }

    public async Task AddAsync(CursoDisciplina cursoDisciplina)
    {
        await _context.CursosDisciplinas.AddAsync(cursoDisciplina);
    }

    public async Task AddRangeAsync(List<CursoDisciplina> cursoDisciplinas)
    {
        await _context.CursosDisciplinas.AddRangeAsync(cursoDisciplinas);
    }

    public void Delete(CursoDisciplina cursoDisciplina)
    {
        _context.CursosDisciplinas.Remove(cursoDisciplina);
    }

    public void DeleteRange(List<CursoDisciplina> cursoDisciplinas)
    {
        _context.CursosDisciplinas.RemoveRange(cursoDisciplinas);
    }
}

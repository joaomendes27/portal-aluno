using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Infrastructure.Data.Repositories;

public class MatriculaRepository : IMatriculaRepository
{
    private readonly AppDbContext _context;

    public MatriculaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Matricula?> GetByIdAsync(int id)
    {
        return await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Matricula?> GetByAlunoRaAsync(int ra)
    {
        return await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .FirstOrDefaultAsync(m => m.Ra == ra);
    }

    public async Task<List<Matricula>> GetAllAsync()
    {
        return await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .ToListAsync();
    }

    public async Task AddAsync(Matricula matricula)
    {
        await _context.Matriculas.AddAsync(matricula);
    }

    public void Update(Matricula matricula)
    {
        _context.Matriculas.Update(matricula);
    }

    public void Delete(Matricula matricula)
    {
        _context.Matriculas.Remove(matricula);
    }
}

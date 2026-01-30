using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Infrastructure.Data.Repositories;

public class ProfessorRepository : IProfessorRepository
{
    private readonly AppDbContext _context;

    public ProfessorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Professor?> GetByIdAsync(int id)
    {
        return await _context.Professores.FindAsync(id);
    }

    public async Task<Professor?> GetByCpfAsync(string cpf)
    {
        return await _context.Professores.FirstOrDefaultAsync(p => p.Cpf == cpf);
    }

    public async Task<Professor?> GetByEmailAsync(string email)
    {
        return await _context.Professores.FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task AddAsync(Professor professor)
    {
        await _context.Professores.AddAsync(professor);
    }

    public async Task DesativarAsync(int id)
    {
        var professor = await GetByIdAsync(id);
        if (professor != null)
        {
            professor.Status = "desativado";
        }
    }
}

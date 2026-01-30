using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Infrastructure.Data.Repositories;

public class AlunoRepository : IAlunoRepository
{
    private readonly AppDbContext _context;

    public AlunoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Aluno?> GetByIdAsync(int ra)
    {
        return await _context.Alunos.FirstOrDefaultAsync(a => a.Ra == ra);
    }

    public async Task<Aluno?> GetByCpfOuEmailAsync(string cpf, string email)
    {
        return await _context.Alunos
            .FirstOrDefaultAsync(a => a.Cpf == cpf || a.Email == email);
    }

    public async Task<Aluno?> GetByCpfAsync(string cpf)
    {
        return await _context.Alunos.FirstOrDefaultAsync(a => a.Cpf == cpf);
    }

    public async Task<Aluno?> GetByEmailAsync(string email)
    {
        return await _context.Alunos.FirstOrDefaultAsync(a => a.Email == email);
    }

    public async Task AddAsync(Aluno aluno)
    {
        await _context.Alunos.AddAsync(aluno);
    }

    public async Task<bool> RaExistsAsync(int ra)
    {
        return await _context.Alunos.AnyAsync(a => a.Ra == ra);
    }

    public async Task DesativarAsync(int ra)
    {
        var aluno = await GetByIdAsync(ra);
        if (aluno != null)
        {
            aluno.Status = "desativado";
        }
    }
}

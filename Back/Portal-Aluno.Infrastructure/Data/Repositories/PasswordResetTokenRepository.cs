using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Infrastructure.Data.Repositories;

public class PasswordResetTokenRepository : IPasswordResetTokenRepository
{
    private readonly AppDbContext _context;

    public PasswordResetTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PasswordResetToken token)
    {
        await _context.Set<PasswordResetToken>().AddAsync(token);
    }

    public async Task<PasswordResetToken?> GetByTokenAsync(string token)
    {
        return await _context.Set<PasswordResetToken>().FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task RemoveAsync(PasswordResetToken token)
    {
        _context.Set<PasswordResetToken>().Remove(token);
    }
}

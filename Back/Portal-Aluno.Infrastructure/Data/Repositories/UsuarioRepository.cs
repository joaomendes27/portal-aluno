using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Infrastructure.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByLoginAsync(string login)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task<bool> LoginExistsAsync(string login)
    {
        return await _context.Usuarios.AnyAsync(u => u.Login == login);
    }

    public async Task AddAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
    }
}

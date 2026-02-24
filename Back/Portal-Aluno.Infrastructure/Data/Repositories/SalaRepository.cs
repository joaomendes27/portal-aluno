using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Infrastructure.Data.Repositories;

public class SalaRepository : ISalaRepository
{
    private readonly AppDbContext _context;

    public SalaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Sala?> GetByIdAsync(int id)
    {
        return await _context.Salas.FindAsync(id);
    }

    public async Task<List<Sala>> GetAllAsync()
    {
        return await _context.Salas.ToListAsync();
    }

    public async Task AddAsync(Sala sala)
    {
        await _context.Salas.AddAsync(sala);
    }

    public void Update(Sala sala)
    {
        _context.Salas.Update(sala);
    }

    public void Delete(Sala sala)
    {
        _context.Salas.Remove(sala);
    }
}

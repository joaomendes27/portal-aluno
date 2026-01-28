using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces.Repositories;

public interface ISalaRepository
{
    Task<Sala?> GetByIdAsync(int id);
    Task<List<Sala>> GetAllAsync();
    Task AddAsync(Sala sala);
    void Update(Sala sala);
    void Delete(Sala sala);
}

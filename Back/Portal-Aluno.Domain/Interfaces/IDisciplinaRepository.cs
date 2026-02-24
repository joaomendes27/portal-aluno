using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface IDisciplinaRepository
{
    Task<Disciplina?> GetByIdAsync(int id);
    Task<List<Disciplina>> GetAllAsync();
    Task AddAsync(Disciplina disciplina);
    Task DeleteAsync(int id);
}

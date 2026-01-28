using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface ITurmaRepository
{
    Task<Turma?> GetByIdAsync(int id);
    Task<List<Turma>> GetAllAsync();
    Task AddAsync(Turma turma);
    void Update(Turma turma);
    void Delete(Turma turma);
}

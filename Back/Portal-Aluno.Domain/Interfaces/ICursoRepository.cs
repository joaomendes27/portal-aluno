using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface ICursoRepository
{
    Task<Curso?> GetByIdAsync(int id);
    Task<List<Curso>> GetAllAsync();
    Task AddAsync(Curso curso);
    Task<Curso?> GetByIdWithDisciplinasAsync(int id);
    void Update(Curso curso);
    void Delete(Curso curso);
}

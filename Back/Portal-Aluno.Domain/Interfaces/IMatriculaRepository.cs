using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface IMatriculaRepository
{
    Task<Matricula?> GetByIdAsync(int id);
    Task<Matricula?> GetByAlunoRaAsync(int alunoRa);
    Task<List<Matricula>> GetAllAsync();
    Task AddAsync(Matricula matricula);
    void Update(Matricula matricula);
    void Delete(Matricula matricula);
}

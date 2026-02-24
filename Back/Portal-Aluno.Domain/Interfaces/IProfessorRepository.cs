using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface IProfessorRepository
{
    Task<Professor?> GetByIdAsync(int id);
    Task<Professor?> GetByCpfAsync(string cpf);
    Task<Professor?> GetByEmailAsync(string email);
    Task<List<Professor>> GetAllAsync();
    Task AddAsync(Professor professor);
    Task DesativarAsync(int id);
}

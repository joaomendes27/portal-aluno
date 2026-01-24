using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces.Repositories;

public interface IProfessorRepository
{
    Task<Professor?> GetByCpfAsync(string cpf);
    Task<Professor?> GetByEmailAsync(string email);
    Task AddAsync(Professor professor);
}

using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface IAlunoRepository
{
    Task<Aluno?> GetByIdAsync(int ra);
    Task<Aluno?> GetByCpfAsync(string cpf);
    Task<Aluno?> GetByEmailAsync(string email);
    Task AddAsync(Aluno aluno);
    Task DesativarAsync(int ra);
}

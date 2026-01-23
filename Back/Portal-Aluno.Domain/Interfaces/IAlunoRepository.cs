using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface IAlunoRepository
{
    Task<Aluno?> GetByCpfAsync(string cpf);
    Task<Aluno?> GetByEmailAsync(string email);
    Task AddAsync(Aluno aluno);
}

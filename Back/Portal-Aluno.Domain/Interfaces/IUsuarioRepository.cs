using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByLoginAsync(string login);
    Task<bool> LoginExistsAsync(string login);
    Task AddAsync(Usuario usuario);
}

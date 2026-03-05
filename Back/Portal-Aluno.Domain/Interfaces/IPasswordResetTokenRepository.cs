using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface IPasswordResetTokenRepository
{
    Task AddAsync(PasswordResetToken token);
    Task<PasswordResetToken?> GetByTokenAsync(string token);
    Task RemoveAsync(PasswordResetToken token);
}

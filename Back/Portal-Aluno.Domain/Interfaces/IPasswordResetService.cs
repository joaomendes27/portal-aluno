namespace Portal_Aluno.Domain.Interfaces;

public interface IPasswordResetService
{
    Task GerarTokenEEnviarEmailAsync(string email);
    Task<bool> RedefinirSenhaAsync(string token, string novaSenha);
}

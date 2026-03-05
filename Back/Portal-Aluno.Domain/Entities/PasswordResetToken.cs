namespace Portal_Aluno.Domain.Entities;

public class PasswordResetToken
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime Expiracao { get; set; }
}

namespace Portal_Aluno.Application.Features.UsuarioFeature.DTOs;

public class RedefinirSenhaRequest
{
    public string Token { get; set; } = string.Empty;
    public string NovaSenha { get; set; } = string.Empty;
}

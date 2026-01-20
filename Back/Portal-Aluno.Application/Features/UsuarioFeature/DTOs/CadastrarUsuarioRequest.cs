namespace Portal_Aluno.Application.Features.UsuarioFeature.DTOs;

public class CadastrarUsuarioRequest
{
    public string Login { get; set; }
    public string Senha { get; set; }
    public string Tipo { get; set; }
    public string? ReferenciaId { get; set; }
}

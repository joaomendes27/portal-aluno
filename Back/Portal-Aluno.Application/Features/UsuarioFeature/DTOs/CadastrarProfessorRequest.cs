namespace Portal_Aluno.Application.Features.UsuarioFeature.DTOs;

public class CadastrarProfessorRequest
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string Titulo { get; set; }
    public string Senha { get; set; }
    public string? Foto { get; set; }
}

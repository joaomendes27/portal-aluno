namespace Portal_Aluno.Application.Features.UsuarioFeature.DTOs;

public class CadastrarAlunoRequest
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Cep { get; set; }
    public string Rua { get; set; }
    public string? Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Pais { get; set; }
    public string Senha { get; set; }
    public string? Foto { get; set; }
    public string? Celular { get; set; }
}

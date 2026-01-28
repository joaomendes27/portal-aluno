namespace Portal_Aluno.Domain.Entities;

public class Aluno
{
    public int Ra { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Cep { get; set; } = string.Empty;
    public string Rua { get; set; } = string.Empty;
    public int Numero { get; set; }
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public string? Foto { get; set; }
    public string Celular { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public Matricula? Matricula { get; set; }
}

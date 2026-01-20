namespace Portal_Aluno.Domain.Entities;

public class Aluno
{
    public string Ra { get; set; } = null!;
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string? Celular { get; set; }
    public string Cep { get; set; }
    public string Rua { get; set; }
    public string? Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Pais { get; set; }
    public string? Foto { get; set; }
    public string Status { get; set; }


    public List<Matricula> Matriculas { get; set; } = new();
}

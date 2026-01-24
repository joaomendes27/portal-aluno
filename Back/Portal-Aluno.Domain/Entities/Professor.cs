namespace Portal_Aluno.Domain.Entities;

public class Professor
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? Foto { get; set; }

    public List<Turma> Turmas { get; set; } = new();
}

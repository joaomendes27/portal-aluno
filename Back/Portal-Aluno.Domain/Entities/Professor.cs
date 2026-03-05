namespace Portal_Aluno.Domain.Entities;

public class Professor
{

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string Titulo { get; set; }

    public string? Foto { get; set; }
    public string Status { get; set; }


    public List<Turma> Turmas { get; set; } = new();
}

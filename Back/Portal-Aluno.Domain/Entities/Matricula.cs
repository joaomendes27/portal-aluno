namespace Portal_Aluno.Domain.Entities;

public class Matricula
{
    public int Id { get; set; }
    public int AlunoRa { get; set; }
    public Aluno Aluno { get; set; } = null!;
    public int CursoId { get; set; }
    public Curso Curso { get; set; } = null!;
    public int? Semestre { get; set; }
    public string? Turno { get; set; }
    public DateTime DataMatricula { get; set; }
    public string? Status { get; set; }
    public string? FormaIngresso { get; set; }

    public List<MatriculaTurma> MatriculaTurmas { get; set; } = new();
}

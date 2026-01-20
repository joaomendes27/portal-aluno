namespace Portal_Aluno.Domain.Entities;

public class MatriculaTurma
{
    public int Id { get; set; }
    public int MatriculaId { get; set; }
    public int TurmaId { get; set; }
    public decimal Nota { get; set; }
    public int? Faltas { get; set; }
    public string? Situacao { get; set; }

    public Matricula? Matricula { get; set; }
    public Turma? Turma { get; set; }
}

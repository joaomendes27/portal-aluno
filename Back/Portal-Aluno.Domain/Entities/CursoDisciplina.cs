namespace Portal_Aluno.Domain.Entities;

public class CursoDisciplina
{
    public int CursoId { get; set; }
    public Curso Curso { get; set; } = null!;
    public int DisciplinaId { get; set; }
    public Disciplina Disciplina { get; set; } = null!;
    public int? Semestre { get; set; }
}

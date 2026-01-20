namespace Portal_Aluno.Domain.Entities;

public class CursoDisciplina
{
    public int Id { get; set; }
    public int CursoId { get; set; }
    public int DisciplinaId { get; set; }
    public int Semestre { get; set; }

    public Curso? Curso { get; set; }
    public Disciplina? Disciplina { get; set; }
}

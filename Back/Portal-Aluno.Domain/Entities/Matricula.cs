namespace Portal_Aluno.Domain.Entities;

public class Matricula
{
    public int Id { get; set; }
    public int Ra { get; set; }
    public int CursoId { get; set; }
    public int Semestre { get; set; }
    public string Turno { get; set; }
    public DateOnly DataMatricula { get; set; }
    public string FormaIngresso { get; set; }
    public string Status { get; set; }


    public Aluno? Aluno { get; set; }
    public Curso? Curso { get; set; }

    public List<MatriculaTurma> MatriculasTurma { get; set; } = new();
}

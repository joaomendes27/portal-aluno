namespace Portal_Aluno.Domain.Entities;

public class Turma
{
    public int Id { get; set; }
    public int CursoId { get; set; }
    public int DisciplinaId { get; set; }
    public int? Semestre { get; set; }
    public int? Ano { get; set; }
    public int ProfessorId { get; set; }
    public string? Horario { get; set; }
    public string? Sala { get; set; }
    public string? Status { get; set; }

    public Curso? Curso { get; set; }
    public Disciplina? Disciplina { get; set; }
    public Professor? Professor { get; set; }

    public List<MatriculaTurma> MatriculasTurma { get; set; } = new();
}

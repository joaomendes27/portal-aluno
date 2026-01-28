namespace Portal_Aluno.Domain.Entities;

public class Turma
{
    public int Id { get; set; }
    public int CursoId { get; set; }
    public Curso Curso { get; set; } = null!;
    public int DisciplinaId { get; set; }
    public Disciplina Disciplina { get; set; } = null!;
    public int? Semestre { get; set; }
    public int? Ano { get; set; }
    public int ProfessorId { get; set; }
    public Professor Professor { get; set; } = null!;
    public string? Status { get; set; }
    public int? Capacidade { get; set; }

    public List<MatriculaTurma> MatriculaTurmas { get; set; } = new();
    public List<HorarioAula> HorariosAula { get; set; } = new();
}

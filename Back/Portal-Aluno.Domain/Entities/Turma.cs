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
    
    public string? DiaSemana { get; set; }
    public TimeOnly? HoraInicio { get; set; }
    public TimeOnly? HoraFim { get; set; }
    public int? SalaId { get; set; }
    public Sala? Sala { get; set; }

    public List<MatriculaTurma> MatriculaTurmas { get; set; } = new();
}

namespace Portal_Aluno.Domain.Entities;

public class Disciplina
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public int? CargaHoraria { get; set; }
    public int? LimiteFaltas { get; set; }

    public List<CursoDisciplina> CursoDisciplinas { get; set; } = new();
    public List<Turma> Turmas { get; set; } = new();
}

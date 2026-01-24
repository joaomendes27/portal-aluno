namespace Portal_Aluno.Domain.Entities;

public class Curso
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Grau { get; set; }
    public int CargaHoraria { get; set; }

    public List<CursoDisciplina> CursoDisciplinas { get; set; } = new();
    public List<Matricula> Matriculas { get; set; } = new();
    public List<Turma> Turmas { get; set; } = new();
}

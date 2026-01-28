using System.Data;

namespace Portal_Aluno.Domain.Entities;

public class HorarioAula
{
    public int Id { get; set; }
    public int TurmaId { get; set; }
    public Turma Turma { get; set; } = null!;
    public int SalaId { get; set; }
    public Sala Sala { get; set; } = null!;
    public string DiaSemana { get; set; } = string.Empty;
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFim { get; set; }
}

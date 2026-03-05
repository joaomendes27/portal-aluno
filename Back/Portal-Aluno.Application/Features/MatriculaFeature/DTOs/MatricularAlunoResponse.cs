namespace Portal_Aluno.Application.Features.MatriculaFeature.DTOs;

public class MatricularAlunoResponse
{
    public int MatriculaId { get; set; }
    public int AlunoRa { get; set; }
    public string CursoNome { get; set; } = string.Empty;
    public int Semestre { get; set; }
    public string Turno { get; set; } = string.Empty;
    public List<TurmaInscritaResponse> TurmasInscritas { get; set; } = new();
    public List<string> Avisos { get; set; } = new();
}

public class TurmaInscritaResponse
{
    public int TurmaId { get; set; }
    public string DisciplinaNome { get; set; } = string.Empty;
    public string ProfessorNome { get; set; } = string.Empty;
    public string? DiaSemana { get; set; }
    public string? HoraInicio { get; set; }
    public string? HoraFim { get; set; }
    public string? Sala { get; set; }
}

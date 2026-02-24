namespace Portal_Aluno.Application.Features.MatriculaFeature.DTOs;

public class MatriculaResponse
{
    public int Id { get; set; }
    public int AlunoRa { get; set; }
    public string AlunoNome { get; set; } = string.Empty;
    public int CursoId { get; set; }
    public string CursoNome { get; set; } = string.Empty;
    public int? Semestre { get; set; }
    public string? Turno { get; set; }
    public DateTime DataMatricula { get; set; }
    public string? Status { get; set; }
    public string? FormaIngresso { get; set; }
}

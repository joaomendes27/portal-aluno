namespace Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;

public class DisciplinaResponse
{
    public int Id { get; set; }
    public int CursoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
    public int LimiteFaltas { get; set; }
    public int Semestre { get; set; }
}

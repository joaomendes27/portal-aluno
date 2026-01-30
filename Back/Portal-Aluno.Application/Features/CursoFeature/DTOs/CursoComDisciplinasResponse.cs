using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;

namespace Portal_Aluno.Application.Features.CursoFeature.DTOs;

public class CursoComDisciplinasResponse
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Grau { get; set; }
    public int? CargaHoraria { get; set; }
    public List<DisciplinaResponse> Disciplinas { get; set; } = new();
}

namespace Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;

public class CadastrarDisciplinasRequest
{
    public int CursoId { get; set; }
    public List<DisciplinaItem> Disciplinas { get; set; } = new();
}

public class DisciplinaItem
{
    public string Nome { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
    public int LimiteFaltas { get; set; }
    public int Semestre { get; set; }
}

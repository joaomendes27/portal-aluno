namespace Portal_Aluno.Application.Features.CursoFeature.DTOs;

public record AdicionarDisciplinasAoCursoRequest(
    int CursoId,
    List<DisciplinaCursoItem> Disciplinas
);

public record DisciplinaCursoItem(
    int DisciplinaId,
    int Semestre
);

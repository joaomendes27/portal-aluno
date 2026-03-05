namespace Portal_Aluno.Application.Features.CursoFeature.DTOs;

public record RemoverDisciplinaDoCursoRequest(
    int CursoId,
    int DisciplinaId
);

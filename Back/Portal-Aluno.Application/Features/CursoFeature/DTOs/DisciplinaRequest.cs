namespace Portal_Aluno.Application.Features.CursoFeature.DTOs;

public record DisciplinaRequest(
    string Nome,
    int CargaHoraria,
    int LimiteFaltas,
    int Semestre
);

namespace Portal_Aluno.Application.Features.MatriculaFeature.DTOs;

public record MatricularAlunoRequest(
    int AlunoRa,
    int CursoId,
    int Semestre,
    string Turno,
    int Ano,
    string FormaIngresso
);

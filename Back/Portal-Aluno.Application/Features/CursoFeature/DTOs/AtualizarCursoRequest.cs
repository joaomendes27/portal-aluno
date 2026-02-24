namespace Portal_Aluno.Application.Features.CursoFeature.DTOs;

public record AtualizarCursoRequest(
    string Nome,
    string Grau,
    int CargaHoraria
);

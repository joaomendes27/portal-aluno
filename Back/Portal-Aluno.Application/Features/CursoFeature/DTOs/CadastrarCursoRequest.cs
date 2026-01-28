using Portal_Aluno.Application.Features.CursoFeature.DTOs;

namespace Portal_Aluno.Application.Features.CursoFeature.DTOs;

public record CadastrarCursoRequest(
    string Nome,
    string Grau,
    int CargaHoraria,
    List<DisciplinaRequest> Disciplinas
);

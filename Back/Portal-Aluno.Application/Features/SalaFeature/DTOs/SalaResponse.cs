namespace Portal_Aluno.Application.Features.SalaFeature.DTOs;

public record SalaResponse(
    int Id,
    int? Andar,
    string Numero
);

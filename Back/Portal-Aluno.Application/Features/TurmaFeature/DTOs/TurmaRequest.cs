using System.Collections.Generic;

namespace Portal_Aluno.Application.Features.TurmaFeature.DTOs;

public record TurmaRequest(
    int CursoId,
    int DisciplinaId,
    int? Semestre,
    int? Ano,
    int ProfessorId,
    string? Status,
    int? Capacidade,
    string? DiaSemana,
    string? HoraInicio, // formato: "HH:mm"
    string? HoraFim,    // formato: "HH:mm"
    int? SalaId
);

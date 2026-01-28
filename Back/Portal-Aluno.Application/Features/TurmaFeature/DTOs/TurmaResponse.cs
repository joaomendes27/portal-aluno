using System.Collections.Generic;

namespace Portal_Aluno.Application.Features.TurmaFeature.DTOs;

public record TurmaResponse(
    int Id,
    int CursoId,
    string CursoNome,
    int DisciplinaId,
    string DisciplinaNome,
    int? Semestre,
    int? Ano,
    int ProfessorId,
    string ProfessorNome,
    string? Status,
    int? Capacidade,
    string? DiaSemana,
    string? HoraInicio,
    string? HoraFim,
    int? SalaId,
    string? SalaNumero
);

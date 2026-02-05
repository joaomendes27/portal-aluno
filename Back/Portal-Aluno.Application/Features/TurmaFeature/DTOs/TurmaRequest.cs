using System.Collections.Generic;

namespace Portal_Aluno.Application.Features.TurmaFeature.DTOs;

public record TurmaRequest(
    int CursoId,
    int DisciplinaId,
    int? Semestre,
    int? Ano,
    string? Turno,
    int ProfessorId,
    string? Status,
    int? Capacidade,
    string? DiaSemana,
    string? HoraInicio,
    string? HoraFim,    
    int? SalaId
);

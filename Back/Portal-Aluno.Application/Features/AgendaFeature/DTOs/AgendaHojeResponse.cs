namespace Portal_Aluno.Application.Features.AgendaFeature.DTOs;

public record AgendaHojeResponse(
    string Disciplina,
    string Professor,
    string Curso,
    string? Sala,
    int? Andar,
    string? DiaSemana,
    string? HoraInicio,
    string? HoraFim
);

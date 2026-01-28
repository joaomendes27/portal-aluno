using MediatR;
using Portal_Aluno.Application.Features.AgendaFeature.DTOs;
using Portal_Aluno.Domain.Interfaces;
using System.Globalization;

namespace Portal_Aluno.Application.Features.AgendaFeature.Queries.Aluno;

public class GetAgendaHojeAlunoQueryHandler : IRequestHandler<GetAgendaHojeAlunoQuery, List<AgendaHojeResponse>>
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly ITurmaRepository _turmaRepository;

    public GetAgendaHojeAlunoQueryHandler(IAlunoRepository alunoRepository, ITurmaRepository turmaRepository)
    {
        _alunoRepository = alunoRepository;
        _turmaRepository = turmaRepository;
    }

    public async Task<List<AgendaHojeResponse>> Handle(GetAgendaHojeAlunoQuery request, CancellationToken cancellationToken)
    {
        var aluno = await _alunoRepository.GetByIdAsync(request.AlunoId);
        if (aluno == null || aluno.Matricula == null)
            return new List<AgendaHojeResponse>();

        var turmas = aluno.Matricula.MatriculaTurmas.Select(mt => mt.Turma).ToList();
        var hoje = DateTime.Now;
        var diaSemana = hoje.ToString("dddd", new CultureInfo("pt-BR"));

        var result = turmas
            .Where(t => string.Equals(t.DiaSemana, diaSemana, StringComparison.OrdinalIgnoreCase))
            .Select(t => new AgendaHojeResponse(
                t.Disciplina?.Nome ?? "",
                t.Professor?.Nome ?? "",
                t.Curso?.Nome ?? "",
                t.Sala?.Numero ?? "",
                t.Sala?.Andar,
                t.DiaSemana,
                t.HoraInicio?.ToString("HH:mm"),
                t.HoraFim?.ToString("HH:mm")
            ))
            .ToList();

        return result;
    }
}

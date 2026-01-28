using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.AgendaFeature.DTOs;
using Portal_Aluno.Application.Features.AgendaFeature.Queries.Aluno;
using Portal_Aluno.Application.Features.AgendaFeature.Queries.Professor;
using System.Security.Claims;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendaController : ControllerBase
{
    private readonly IMediator _mediator;

    public AgendaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "aluno")]
    [HttpGet("alunos/minha-agenda/hoje")]
    public async Task<ActionResult<List<AgendaHojeResponse>>> GetAgendaHojeAluno()
    {
        var alunoIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(alunoIdStr, out var alunoId))
            return Unauthorized();
        var result = await _mediator.Send(new GetAgendaHojeAlunoQuery(alunoId));
        return Ok(result);
    }

    [Authorize(Roles = "professor")]
    [HttpGet("professores/minha-agenda/hoje")]
    public async Task<ActionResult<List<AgendaHojeResponse>>> GetAgendaHojeProfessor()
    {
        var professorIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(professorIdStr, out var professorId))
            return Unauthorized();
        var result = await _mediator.Send(new GetAgendaHojeProfessorQuery(professorId));
        return Ok(result);
    }
}

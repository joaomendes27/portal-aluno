using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.AgendaFeature.DTOs;
using Portal_Aluno.Application.Features.AgendaFeature.Queries.Aluno;
using Portal_Aluno.Application.Features.AgendaFeature.Queries.Professor;
using System.Security.Claims;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize]
public class AgendaController : ControllerBase
{
    private readonly IMediator _mediator;

    public AgendaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "aluno")]
    [HttpGet("Alunos/Hoje")]
    public async Task<ActionResult<List<AgendaHojeResponse>>> GetAgendaHojeAluno()
    {
        var alunoIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(alunoIdStr, out var alunoId))
            return Unauthorized();
        var result = await _mediator.Send(new GetAgendaHojeAlunoQuery(alunoId));
        return Ok(result);
    }

    [Authorize(Roles = "professor")]
    [HttpGet("Professores/Hoje")]
    public async Task<ActionResult<List<AgendaHojeResponse>>> GetAgendaHojeProfessor()
    {
        var professorIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(professorIdStr, out var professorId))
            return Unauthorized();
        var result = await _mediator.Send(new GetAgendaHojeProfessorQuery(professorId));
        return Ok(result);
    }
}

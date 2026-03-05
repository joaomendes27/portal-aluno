using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.TurmaFeature.Commands.CreateTurma;
using Portal_Aluno.Application.Features.TurmaFeature.Commands.UpdateTurma;
using Portal_Aluno.Application.Features.TurmaFeature.Commands.DeleteTurma;
using Portal_Aluno.Application.Features.TurmaFeature.DTOs;
using Portal_Aluno.Application.Features.TurmaFeature.Queries.GetAllTurmas;
using Portal_Aluno.Application.Features.TurmaFeature.Queries.GetTurmaById;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize]
public class TurmasController : ControllerBase
{
    private readonly IMediator _mediator;

    public TurmasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ListarTurmas")]
    public async Task<ActionResult<List<TurmaResponse>>> GetAllTurmas()
    {
        var result = await _mediator.Send(new GetAllTurmasQuery());
        return Ok(result);
    }

    [HttpGet("BuscarTurmaPorId/{id}")]
    public async Task<ActionResult<TurmaResponse>> GetTurmasById(int id)
    {
        var result = await _mediator.Send(new GetTurmaByIdQuery(id));
        return Ok(result);
    }

    [HttpPost("CriarTurma")]
    public async Task<ActionResult<TurmaResponse>> Create([FromBody] TurmaRequest request)
    {
        var result = await _mediator.Send(new CriarTurmaCommand(request));
        return CreatedAtAction(nameof(GetTurmasById), new { id = result.Id }, result);
    }

    [HttpPut("AtualizarTurma")]
    public async Task<ActionResult<TurmaResponse>> UpdateTurma(int id, [FromBody] TurmaRequest request)
    {
        var result = await _mediator.Send(new AtualizarTurmaCommand(id, request));
        return Ok(result);
    }

    [HttpDelete("DeletarTurma/{id}")]
    public async Task<IActionResult> DeleteTurma(int id)
    {
        await _mediator.Send(new DeletarTurmaCommand(id));
        return NoContent();
    }
}

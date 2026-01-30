using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.DisciplinaFeature.Commands;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;
using Portal_Aluno.Application.Features.DisciplinaFeature.Queries;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class DisciplinaController : ControllerBase
{
    private readonly IMediator _mediator;

    public DisciplinaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CadastrarDisciplina")]
    public async Task<IActionResult> CadastrarDisciplina([FromBody] DisciplinaRequest request)
    {
        var id = await _mediator.Send(new CreateDisciplinaCommand(request));
        return CreatedAtAction(nameof(BuscarDisciplinaPorId), new { id }, request);
    }

    [HttpPut("AtualizarDisciplina/{id}")]
    public async Task<IActionResult> AtualizarDisciplina(int id, [FromBody] DisciplinaRequest request)
    {
        await _mediator.Send(new UpdateDisciplinaCommand(id, request));
        return NoContent();
    }

    [HttpDelete("DeletarDisciplina/{id}")]
    public async Task<IActionResult> DeletarDisciplina(int id)
    {
        await _mediator.Send(new DeleteDisciplinaCommand(id));
        return NoContent();
    }

    [HttpGet("ListarDisciplinas")]
    public async Task<ActionResult<List<DisciplinaResponse>>> ListarDisciplinas()
    {
        var result = await _mediator.Send(new GetAllDisciplinasQuery());
        return Ok(result);
    }

    [HttpGet("BuscarDisciplinaPorId/{id}")]
    public async Task<ActionResult<DisciplinaResponse>> BuscarDisciplinaPorId(int id)
    {
        var result = await _mediator.Send(new GetDisciplinaByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }
}
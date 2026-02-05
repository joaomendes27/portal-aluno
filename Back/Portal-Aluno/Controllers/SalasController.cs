using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;
using Portal_Aluno.Application.Features.SalaFeature.Queries;
using Portal_Aluno.Application.Features.SalaFeature.Commands.UpdateSala;
using Portal_Aluno.Application.Features.SalaFeature.Commands.DeleteSala;
using Portal_Aluno.Application.Features.SalaFeature.Commands.CreateSala;



namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize]
public class SalasController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ListarSalas")]
    public async Task<ActionResult<List<SalaResponse>>> GetAllSalas()
    {
        var result = await _mediator.Send(new GetAllSalasQuery());
        return Ok(result);
    }

    [HttpGet("BuscarSalaPorId/{id}")]
    public async Task<ActionResult<SalaResponse>> GetSalasById(int id)
    {
        var result = await _mediator.Send(new GetSalaByIdQuery(id));
        return Ok(result);
    }

    [HttpPost("CriarSala")]
    public async Task<ActionResult<SalaResponse>> CreateSala([FromBody] SalaRequest request)
    {
        var result = await _mediator.Send(new CreateSalaCommand(request));
        return CreatedAtAction(nameof(GetSalasById), new { id = result.Id }, result);
    }

    [HttpPut("AtualizarSala")]
    public async Task<ActionResult<SalaResponse>> UpdateSala(int id, [FromBody] SalaRequest request)
    {
        var result = await _mediator.Send(new UpdateSalaCommand(id, request));
        return Ok(result);
    }

    [HttpDelete("DeletarSala/{id}")]
    public async Task<IActionResult> DeleteSala(int id)
    {
        await _mediator.Send(new DeleteSalaCommand(id));
        return NoContent();
    }
}

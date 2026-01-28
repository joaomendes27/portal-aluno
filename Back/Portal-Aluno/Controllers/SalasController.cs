using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.SalaFeature.Commands;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;
using Portal_Aluno.Application.Features.SalaFeature.Queries;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalasController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<SalaResponse>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllSalasQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SalaResponse>> GetById(int id)
    {
        var result = await _mediator.Send(new GetSalaByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<SalaResponse>> Create([FromBody] SalaRequest request)
    {
        var result = await _mediator.Send(new CreateSalaCommand(request));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SalaResponse>> Update(int id, [FromBody] SalaRequest request)
    {
        var result = await _mediator.Send(new UpdateSalaCommand(id, request));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteSalaCommand(id));
        return NoContent();
    }
}

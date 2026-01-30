using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.DisciplinaFeature.Commands;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;
using Portal_Aluno.Application.Features.DisciplinaFeature.Queries;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class DisciplinasController : ControllerBase
{
    private readonly IMediator _mediator;

    public DisciplinasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DisciplinaRequest request)
    {
        var id = await _mediator.Send(new CreateDisciplinaCommand(request));
        return CreatedAtAction(nameof(GetById), new { id }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DisciplinaRequest request)
    {
        await _mediator.Send(new UpdateDisciplinaCommand(id, request));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteDisciplinaCommand(id));
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<List<DisciplinaResponse>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllDisciplinasQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DisciplinaResponse>> GetById(int id)
    {
        var result = await _mediator.Send(new GetDisciplinaByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }
}

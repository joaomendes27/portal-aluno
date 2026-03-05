using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Features.DisciplinaFeature.Commands.DeleteDisciplina;
using Features.DisciplinaFeature.Commands.UpdateDisciplina;
using Portal_Aluno.Application.Features.DisciplinaFeature.Commands.CadastrarDisciplinas;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;
using Portal_Aluno.Application.Features.DisciplinaFeature.Queries;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize]
public class DisciplinaController : ControllerBase
{
    private readonly IMediator _mediator;

    public DisciplinaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CadastrarDisciplinas")]
    public async Task<IActionResult> CadastrarDisciplinas([FromBody] CadastrarDisciplinasRequest request)
    {
        var ids = await _mediator.Send(new CadastrarDisciplinasCommand(request));
        return Ok(new { Message = "Disciplinas cadastradas com sucesso.", DisciplinaIds = ids });
    }

    [HttpPut("AtualizarDisciplina/{id}")]
    public async Task<IActionResult> AtualizarDisciplina(int id, [FromBody] DisciplinaRequest request)
    {
        await _mediator.Send(new AtualizarDisciplinaCommand(id, request));
        return NoContent();
    }

    [HttpDelete("DeletarDisciplina/{id}")]
    public async Task<IActionResult> DeletarDisciplina(int id)
    {
        await _mediator.Send(new DeletarDisciplinaCommand(id));
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
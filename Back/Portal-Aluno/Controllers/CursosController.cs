using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.CursoFeature.Commands;
using Portal_Aluno.Application.Features.CursoFeature.DTOs;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class CursosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICursoRepository _cursoRepository;

    public CursosController(IMediator mediator, ICursoRepository cursoRepository)
    {
        _mediator = mediator;
        _cursoRepository = cursoRepository;
    }

    [HttpGet("ListarCursos")]
    public async Task<IActionResult> GetAllCursos()
    {
        var cursos = await _cursoRepository.GetAllAsync();
        return Ok(cursos);
    }

    [HttpPost("CadastrarCurso")]
    public async Task<IActionResult> CadastrarCurso([FromBody] CadastrarCursoRequest request)
    {
        var command = new CadastrarCursoCommand(request);
        var cursoId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCursoById), new { id = cursoId }, request);
    }

    [HttpGet("BuscarCursoPorId/{id}")]
    public IActionResult GetCursoById(int id)
    {
        return Ok(new { Id = id, Message = "Endpoint de busca a ser implementado." });
    }
}

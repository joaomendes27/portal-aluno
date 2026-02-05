using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.CursoFeature.Commands.CadastrarCurso;
using Portal_Aluno.Application.Features.CursoFeature.Commands.AtualizarCurso;
using Portal_Aluno.Application.Features.CursoFeature.Commands.DeletarCurso;
using Portal_Aluno.Application.Features.CursoFeature.DTOs;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize]
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
    public async Task<IActionResult> GetCursoById(int id)
    {
        var curso = await _cursoRepository.GetByIdWithDisciplinasAsync(id);
        if (curso == null) return NotFound();

        var response = new CursoComDisciplinasResponse
        {
            Id = curso.Id,
            Nome = curso.Nome,
            Grau = curso.Grau,
            CargaHoraria = curso.CargaHoraria,
            Disciplinas = curso.CursoDisciplinas.Select(cd => new DisciplinaResponse
            {
                Id = cd.Disciplina.Id,
                Nome = cd.Disciplina.Nome ?? string.Empty,
                CargaHoraria = cd.Disciplina.CargaHoraria ?? 0,
                LimiteFaltas = cd.Disciplina.LimiteFaltas ?? 0,
                Semestre = cd.Semestre ?? 0,
                CursoId = curso.Id
            }).ToList()
        };

        return Ok(response);
    }

    [HttpPut("AtualizarCurso/{id}")]
    public async Task<IActionResult> AtualizarCurso(int id, [FromBody] AtualizarCursoRequest request)
    {
        await _mediator.Send(new AtualizarCursoCommand(id, request));
        return NoContent();
    }

    [HttpDelete("DeletarCurso/{id}")]
    public async Task<IActionResult> DeletarCurso(int id)
    {
        await _mediator.Send(new DeletarCursoCommand(id));
        return NoContent();
    }
}

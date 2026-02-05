using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.MatriculaFeature.Commands.MatricularAluno;
using Portal_Aluno.Application.Features.MatriculaFeature.Commands.InscreverEmTurma;
using Portal_Aluno.Application.Features.MatriculaFeature.DTOs;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize]
public class MatriculasController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMatriculaRepository _matriculaRepository;

    public MatriculasController(IMediator mediator, IMatriculaRepository matriculaRepository)
    {
        _mediator = mediator;
        _matriculaRepository = matriculaRepository;
    }

    [HttpPost("MatricularAluno")]
    public async Task<IActionResult> MatricularAluno([FromBody] MatricularAlunoRequest request)
    {
        var matriculaId = await _mediator.Send(new MatricularAlunoCommand(request));
        return CreatedAtAction(nameof(GetMatriculaPorId), new { id = matriculaId }, request);
    }

    [HttpPost("InscreverEmTurma")]
    public async Task<IActionResult> InscreverEmTurma([FromBody] InscreverEmTurmaRequest request)
    {
        var inscricaoId = await _mediator.Send(new InscreverEmTurmaCommand(request));
        return Ok(new { Id = inscricaoId, Message = "Aluno inscrito na turma com sucesso." });
    }

    [HttpGet("BuscarMatriculaPorId/{id}")]
    public async Task<IActionResult> GetMatriculaPorId(int id)
    {
        var matricula = await _matriculaRepository.GetByIdAsync(id);
        if (matricula == null) return NotFound();

        var response = new MatriculaResponse
        {
            Id = matricula.Id,
            AlunoRa = matricula.Ra,
            AlunoNome = matricula.Aluno?.Nome ?? string.Empty,
            CursoId = matricula.CursoId,
            CursoNome = matricula.Curso?.Nome ?? string.Empty,
            Semestre = matricula.Semestre,
            Turno = matricula.Turno,
            DataMatricula = matricula.DataMatricula,
            Status = matricula.Status,
            FormaIngresso = matricula.FormaIngresso
        };

        return Ok(response);
    }

    [HttpGet("BuscarMatriculaPorAluno/{ra}")]
    public async Task<IActionResult> GetMatriculaPorAluno(int ra)
    {
        var matricula = await _matriculaRepository.GetByAlunoRaAsync(ra);
        if (matricula == null) return NotFound();

        var response = new MatriculaResponse
        {
            Id = matricula.Id,
            AlunoRa = matricula.Ra,
            AlunoNome = matricula.Aluno?.Nome ?? string.Empty,
            CursoId = matricula.CursoId,
            CursoNome = matricula.Curso?.Nome ?? string.Empty,
            Semestre = matricula.Semestre,
            Turno = matricula.Turno,
            DataMatricula = matricula.DataMatricula,
            Status = matricula.Status,
            FormaIngresso = matricula.FormaIngresso
        };

        return Ok(response);
    }

    [HttpGet("ListarMatriculas")]
    public async Task<IActionResult> GetAllMatriculas()
    {
        var matriculas = await _matriculaRepository.GetAllAsync();
        
        var response = matriculas.Select(m => new MatriculaResponse
        {
            Id = m.Id,
            AlunoRa = m.Ra,
            AlunoNome = m.Aluno?.Nome ?? string.Empty,
            CursoId = m.CursoId,
            CursoNome = m.Curso?.Nome ?? string.Empty,
            Semestre = m.Semestre,
            Turno = m.Turno,
            DataMatricula = m.DataMatricula,
            Status = m.Status,
            FormaIngresso = m.FormaIngresso
        }).ToList();

        return Ok(response);
    }
}

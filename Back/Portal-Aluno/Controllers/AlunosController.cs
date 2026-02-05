using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize]
public class AlunosController : ControllerBase
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AlunosController(IAlunoRepository alunoRepository, IUnitOfWork unitOfWork)
    {
        _alunoRepository = alunoRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("BuscarPorRa/{ra}")]
    public async Task<IActionResult> BuscarAlunoPorRa(int ra)
    {
        var aluno = await _alunoRepository.GetByIdAsync(ra);
        if (aluno == null) return NotFound();
        return Ok(aluno);
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> ListarAlunos()
    {
        var alunos = await _alunoRepository.GetAllAsync();
        return Ok(alunos);
    }

    [HttpDelete("Desativar/{ra}")]
    public async Task<IActionResult> DesativarAluno(int ra)
    {
        await _alunoRepository.DesativarAsync(ra);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize]
public class ProfessoresController : ControllerBase
{
    private readonly IProfessorRepository _professorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProfessoresController(IProfessorRepository professorRepository, IUnitOfWork unitOfWork)
    {
        _professorRepository = professorRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("BuscarPorId/{id}")]
    public async Task<IActionResult> BuscarProfessorPorId(int id)
    {
        var professor = await _professorRepository.GetByIdAsync(id);
        if (professor == null) return NotFound();
        return Ok(professor);
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> ListarProfessores()
    {
        var professores = await _professorRepository.GetAllAsync();
        return Ok(professores);
    }

    [HttpDelete("Desativar/{id}")]
    public async Task<IActionResult> DesativarProfessor(int id)
    {
        await _professorRepository.DesativarAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}

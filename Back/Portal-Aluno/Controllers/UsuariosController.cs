using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAdmin;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAluno;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarProfessor;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.LoginUsuario;
using Portal_Aluno.Application.Features.UsuarioFeature.DTOs;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("cadastrar-aluno")]
    public async Task<IActionResult> CadastrarAluno([FromBody] CadastrarAlunoRequest request)
    {
        await _mediator.Send(new CadastrarAlunoCommand(request));
        return Ok("Aluno cadastrado com sucesso.");
    }

    [HttpPost("cadastrar-professor")]
    public async Task<IActionResult> CadastrarProfessor([FromBody] CadastrarProfessorRequest request)
    {
        await _mediator.Send(new CadastrarProfessorCommand(request));
        return Ok("Professor cadastrado com sucesso.");
    }

    [HttpPost("cadastrar-admin")]
    public async Task<IActionResult> CadastrarAdmin([FromBody] CadastrarAdminRequest request)
    {
        await _mediator.Send(new CadastrarAdminCommand(request));
        return Ok("Administrador cadastrado com sucesso.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _mediator.Send(new LoginUsuarioCommand(request));
        return Ok(response);
    }
}

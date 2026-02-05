using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAdmin;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAluno;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarProfessor;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.LoginUsuario;
using Portal_Aluno.Application.Features.UsuarioFeature.DTOs;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CadastrarAluno")]
    [AllowAnonymous]
    public async Task<IActionResult> CadastrarAluno([FromBody] CadastrarAlunoRequest request)
    {
        await _mediator.Send(new CadastrarAlunoCommand(request));
        return Ok("Aluno cadastrado com sucesso.");
    }

    [HttpPost("CadastrarProfessor")]
    [AllowAnonymous]
    public async Task<IActionResult> CadastrarProfessor([FromBody] CadastrarProfessorRequest request)
    {
        await _mediator.Send(new CadastrarProfessorCommand(request));
        return Ok("Professor cadastrado com sucesso.");
    }

    [HttpPost("CadastrarAdmin")]
    [AllowAnonymous]
    public async Task<IActionResult> CadastrarAdmin([FromBody] CadastrarAdminRequest request)
    {
        await _mediator.Send(new CadastrarAdminCommand(request));
        return Ok("Administrador cadastrado com sucesso.");
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _mediator.Send(new LoginUsuarioCommand(request));
        return Ok(response);
    }

    [HttpPost("EsqueciSenha")]
    [AllowAnonymous]
    public async Task<IActionResult> EsqueciSenha([FromBody] EsqueciSenhaRequest request, [FromServices] IPasswordResetService passwordResetService)
    {
        try
        {
            await passwordResetService.GerarTokenEEnviarEmailAsync(request.Email);
            return Ok("E-mail de redefinição enviado.");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("RedefinirSenha")]
    [AllowAnonymous]
    public async Task<IActionResult> RedefinirSenha([FromBody] RedefinirSenhaRequest request, [FromServices] IPasswordResetService passwordResetService)
    {
        var resultado = await passwordResetService.RedefinirSenhaAsync(request.Token, request.NovaSenha);
        if (!resultado) return BadRequest("Token inválido ou expirado.");
        return Ok("Senha redefinida com sucesso.");
    }
}

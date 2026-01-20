using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarUsuario;
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

    [HttpPost("cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] CadastrarUsuarioRequest request)
    {
        try
        {
            var command = new CadastrarUsuarioCommand(request);
            await _mediator.Send(command);
            return Ok("Usuário cadastrado com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao cadastrar usuário: {ex.Message}");
        }
    }
    //Try catchs temporários, posteriormente vou implementar tratamento global de erros

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var command = new LoginUsuarioCommand(request);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao realizar login: {ex.Message}");
        }
    }
}

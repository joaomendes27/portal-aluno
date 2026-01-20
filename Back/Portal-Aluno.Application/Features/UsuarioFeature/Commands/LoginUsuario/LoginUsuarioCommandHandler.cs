using MediatR;
using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Application.Features.UsuarioFeature.DTOs;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.LoginUsuario;

public class LoginUsuarioCommandHandler : IRequestHandler<LoginUsuarioCommand, LoginResponse>
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public LoginUsuarioCommandHandler(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha))
        {
            throw new UnauthorizedAccessException("Usuário ou senha inválidos.");
        }

        var token = _tokenService.GenerateJwtToken(usuario);

        return new LoginResponse { Token = token };
    }
}
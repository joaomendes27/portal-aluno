using MediatR;
using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarUsuario;

public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, Unit>
{
    private readonly AppDbContext _context;

    public CadastrarUsuarioCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuarioExistente = await _context.Usuarios.AnyAsync(u => u.Login == request.Dto.Login, cancellationToken);

        if (usuarioExistente)
        {
            throw new Exception("Este login já está em uso.");
        }

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Senha);

        var novoUsuario = new Usuario
        {
            Login = request.Dto.Login,
            Senha = senhaHash,
            Tipo = request.Dto.Tipo,
            Referencia = request.Dto.ReferenciaId
        };

        await _context.Usuarios.AddAsync(novoUsuario, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

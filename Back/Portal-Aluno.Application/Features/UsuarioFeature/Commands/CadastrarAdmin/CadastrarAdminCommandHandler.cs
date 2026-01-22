using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAdmin;

public class CadastrarAdminCommandHandler : IRequestHandler<CadastrarAdminCommand, Unit>
{
    private readonly AppDbContext _context;

    public CadastrarAdminCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CadastrarAdminCommand request, CancellationToken cancellationToken)
    {
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Senha);
        var novoUsuario = new Usuario
        {
            Login = request.Dto.Email,
            Senha = senhaHash,
            Tipo = "adm"
        };
        await _context.Usuarios.AddAsync(novoUsuario, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

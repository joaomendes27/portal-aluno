using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarProfessor;

public class CadastrarProfessorCommandHandler : IRequestHandler<CadastrarProfessorCommand, Unit>
{
    private readonly AppDbContext _context;

    public CadastrarProfessorCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CadastrarProfessorCommand request, CancellationToken cancellationToken)
    {
        var novoProfessor = new Professor
        {
            Nome = request.Dto.Nome,
            Email = request.Dto.Email,
            Titulo = request.Dto.Titulo,
            Foto = request.Dto.Foto,
            Status = "Ativo"
        };
        await _context.Professores.AddAsync(novoProfessor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken); 

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Senha);
        var novoUsuario = new Usuario
        {
            Login = request.Dto.Email,
            Senha = senhaHash,
            Tipo = "professor",
            ReferenciaId = novoProfessor.Id.ToString()
        };
        await _context.Usuarios.AddAsync(novoUsuario, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

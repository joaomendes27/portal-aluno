using MediatR;
using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Infrastructure.Data.DbContexts;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAluno;

public class CadastrarAlunoCommandHandler : IRequestHandler<CadastrarAlunoCommand, Unit>
{
    private readonly AppDbContext _context;

    public CadastrarAlunoCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CadastrarAlunoCommand request, CancellationToken cancellationToken)
    {
        //Temporário, vou mudar a lógica de gerar o RA
        string ra;
        do
        {
            ra = new Random().Next(100000, 999999).ToString();
        } while (await _context.Alunos.AnyAsync(a => a.Ra == ra, cancellationToken));

        var novoAluno = new Aluno
        {
            Ra = ra,
            Nome = request.Dto.Nome,
            Email = request.Dto.Email,
            DataNascimento = request.Dto.DataNascimento,
            Cep = request.Dto.Cep,
            Rua = request.Dto.Rua,
            Numero = request.Dto.Numero,
            Bairro = request.Dto.Bairro,
            Cidade = request.Dto.Cidade,
            Estado = request.Dto.Estado,
            Pais = request.Dto.Pais,
            Foto = request.Dto.Foto,
            Celular = request.Dto.Celular,
            Status = "Ativo"
        };
        await _context.Alunos.AddAsync(novoAluno, cancellationToken);

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Senha);
        var novoUsuario = new Usuario
        {
            Login = ra,
            Senha = senhaHash,
            Tipo = "aluno",
            ReferenciaId = ra
        };
        await _context.Usuarios.AddAsync(novoUsuario, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

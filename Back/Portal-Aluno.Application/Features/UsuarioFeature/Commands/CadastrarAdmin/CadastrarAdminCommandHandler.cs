using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAdmin;

public class CadastrarAdminCommandHandler : IRequestHandler<CadastrarAdminCommand, Unit>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CadastrarAdminCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CadastrarAdminCommand request, CancellationToken cancellationToken)
    {
        if (await _usuarioRepository.LoginExistsAsync(request.Dto.Email))
        {
            throw new Exception("Este email já está em uso como login.");
        }

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Senha);
        var novoUsuario = new Usuario
        {
            Login = request.Dto.Email,
            Senha = senhaHash,
            Tipo = "adm"
        };
        await _usuarioRepository.AddAsync(novoUsuario);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

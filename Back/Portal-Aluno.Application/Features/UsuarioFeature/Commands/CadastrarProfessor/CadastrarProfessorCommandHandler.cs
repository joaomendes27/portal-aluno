using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarProfessor;

public class CadastrarProfessorCommandHandler : IRequestHandler<CadastrarProfessorCommand, Unit>
{
    private readonly IProfessorRepository _professorRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CadastrarProfessorCommandHandler(IProfessorRepository professorRepository, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _professorRepository = professorRepository;
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CadastrarProfessorCommand request, CancellationToken cancellationToken)
    {
        if (await _professorRepository.GetByCpfAsync(request.Dto.Cpf) is { Status: "Ativo" })
        {
            throw new Exception("CPF já cadastrado para um professor ativo.");
        }

        if (await _professorRepository.GetByEmailAsync(request.Dto.Email) is { Status: "Ativo" })
        {
            throw new Exception("Email já cadastrado para um professor ativo.");
        }

        if (await _usuarioRepository.LoginExistsAsync(request.Dto.Email))
        {
            var professorComLogin = await _professorRepository.GetByEmailAsync(request.Dto.Email);
            if (professorComLogin == null || professorComLogin.Status == "Ativo")
            {
                throw new Exception("Este email já está em uso como login de um usuário ativo.");
            }
        }

        var professorExiste = await _professorRepository.GetByCpfAsync(request.Dto.Cpf) ?? await _professorRepository.GetByEmailAsync(request.Dto.Email);

        if (professorExiste != null)
        {
            professorExiste.Nome = request.Dto.Nome;
            professorExiste.Email = request.Dto.Email;
            professorExiste.Cpf = request.Dto.Cpf;
            professorExiste.Titulo = request.Dto.Titulo;
            professorExiste.Foto = request.Dto.Foto;
            professorExiste.Status = "Ativo";

            var usuario = await _usuarioRepository.GetByLoginAsync(professorExiste.Email);
            if (usuario == null)
            {
                usuario = new Usuario
                {
                    Login = professorExiste.Email,
                    Tipo = "professor",
                    ReferenciaId = professorExiste.Id.ToString()
                };
                await _usuarioRepository.AddAsync(usuario);
            }

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(request.Dto.Senha);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        var novoProfessor = new Professor
        {
            Nome = request.Dto.Nome,
            Email = request.Dto.Email,
            Cpf = request.Dto.Cpf,
            Titulo = request.Dto.Titulo,
            Foto = request.Dto.Foto,
            Status = "Ativo"
        };
        await _professorRepository.AddAsync(novoProfessor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var senhaHashNovo = BCrypt.Net.BCrypt.HashPassword(request.Dto.Senha);
        var novoUsuarioCrido = new Usuario
        {
            Login = request.Dto.Email,
            Senha = senhaHashNovo,
            Tipo = "professor",
            ReferenciaId = novoProfessor.Id.ToString(),
            Email = request.Dto.Email
        };
        await _usuarioRepository.AddAsync(novoUsuarioCrido);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

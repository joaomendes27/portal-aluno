using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.UsuarioFeature.Commands.CadastrarAluno;

public class CadastrarAlunoCommandHandler : IRequestHandler<CadastrarAlunoCommand, Unit>
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CadastrarAlunoCommandHandler(IAlunoRepository alunoRepository, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _alunoRepository = alunoRepository;
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CadastrarAlunoCommand request, CancellationToken cancellationToken)
    {
        if (await _alunoRepository.GetByCpfAsync(request.Dto.Cpf) is { Status: "Ativo" })
        {
            throw new Exception("CPF já cadastrado para um aluno ativo.");
        }

        if (await _alunoRepository.GetByEmailAsync(request.Dto.Email) is { Status: "Ativo" })
        {
            throw new Exception("Email já cadastrado para um aluno ativo.");
        }

        var alunoExiste = await _alunoRepository.GetByCpfAsync(request.Dto.Cpf) ?? await _alunoRepository.GetByEmailAsync(request.Dto.Email);

        if (alunoExiste != null)
        {

            alunoExiste.Nome = request.Dto.Nome;
            alunoExiste.Email = request.Dto.Email;
            alunoExiste.Cpf = request.Dto.Cpf;
            alunoExiste.DataNascimento = request.Dto.DataNascimento;
            alunoExiste.Cep = request.Dto.Cep;
            alunoExiste.Rua = request.Dto.Rua;
            alunoExiste.Numero = request.Dto.Numero;
            alunoExiste.Bairro = request.Dto.Bairro;
            alunoExiste.Cidade = request.Dto.Cidade;
            alunoExiste.Estado = request.Dto.Estado;
            alunoExiste.Pais = request.Dto.Pais;
            alunoExiste.Foto = request.Dto.Foto;
            alunoExiste.Celular = request.Dto.Celular;
            alunoExiste.Status = "Ativo";

            var usuario = await _usuarioRepository.GetByLoginAsync(alunoExiste.Ra.ToString());
            if (usuario != null)
            {
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(request.Dto.Senha);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }


        var novoAluno = new Aluno
        {
            Nome = request.Dto.Nome,
            Email = request.Dto.Email,
            Cpf = request.Dto.Cpf,
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
        await _alunoRepository.AddAsync(novoAluno);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Senha);
        var novoUsuario = new Usuario
        {
            Login = novoAluno.Ra.ToString(),
            Senha = senhaHash,
            Tipo = "aluno",
            ReferenciaId = novoAluno.Ra.ToString()
        };
        await _usuarioRepository.AddAsync(novoUsuario);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

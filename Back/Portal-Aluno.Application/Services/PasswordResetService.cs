using System.Security.Cryptography;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Services;

public class PasswordResetService : IPasswordResetService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordResetTokenRepository _tokenRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public PasswordResetService(
        IUsuarioRepository usuarioRepository,
        IPasswordResetTokenRepository tokenRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _tokenRepository = tokenRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task GerarTokenEEnviarEmailAsync(string email)
    {
        var usuario = await _usuarioRepository.GetByLoginAsync(email);
        if (usuario == null)
            throw new Exception("Usuário não encontrado.");

        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        var expiracao = DateTime.UtcNow.AddHours(1);
        var resetToken = new PasswordResetToken
        {
            Email = usuario.Email,
            Token = token,
            Expiracao = expiracao
        };
        await _tokenRepository.AddAsync(resetToken);
        await _unitOfWork.SaveChangesAsync();

        // TODO: Trocar o link abaixo para a URL real da tela de redefinição de senha do frontend
        var link = $"https://seusite.com/redefinir-senha?token={token}";
        var body = $"Clique no link para redefinir sua senha: <a href='{link}'>Redefinir Senha</a>";
        await _emailService.SendEmailAsync(usuario.Email, "Redefinição de Senha", body);
    }

    public async Task<bool> RedefinirSenhaAsync(string token, string novaSenha)
    {
        var resetToken = await _tokenRepository.GetByTokenAsync(token);
        if (resetToken == null || resetToken.Expiracao < DateTime.UtcNow)
            return false;

        var usuario = await _usuarioRepository.GetByLoginAsync(resetToken.Email);
        if (usuario == null)
            return false;

        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(novaSenha);
        await _tokenRepository.RemoveAsync(resetToken);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}

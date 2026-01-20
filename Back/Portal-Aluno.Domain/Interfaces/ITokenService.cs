using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(Usuario usuario);
}

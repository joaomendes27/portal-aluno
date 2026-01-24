using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

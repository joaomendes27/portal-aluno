using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface IMatriculaTurmaRepository
{
    Task<MatriculaTurma?> GetByIdAsync(int id);
    Task<MatriculaTurma?> GetByMatriculaAndTurmaAsync(int matriculaId, int turmaId);
    Task<List<MatriculaTurma>> GetByMatriculaIdAsync(int matriculaId);
    Task<List<MatriculaTurma>> GetByTurmaIdAsync(int turmaId);
    Task AddAsync(MatriculaTurma matriculaTurma);
    void Update(MatriculaTurma matriculaTurma);
    void Delete(MatriculaTurma matriculaTurma);
}

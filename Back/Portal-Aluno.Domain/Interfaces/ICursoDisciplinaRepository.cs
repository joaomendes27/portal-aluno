using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;

public interface ICursoDisciplinaRepository
{
    Task<List<CursoDisciplina>> GetByCursoIdAsync(int cursoId);
    Task<List<CursoDisciplina>> GetByDisciplinaIdAsync(int disciplinaId);
    Task<CursoDisciplina?> GetByCursoAndDisciplinaAsync(int cursoId, int disciplinaId);
    Task AddAsync(CursoDisciplina cursoDisciplina);
    Task AddRangeAsync(List<CursoDisciplina> cursoDisciplinas);
    void Delete(CursoDisciplina cursoDisciplina);
    void DeleteRange(List<CursoDisciplina> cursoDisciplinas);
}

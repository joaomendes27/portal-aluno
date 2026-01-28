using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Domain.Interfaces;


    public interface ICursoRepository
    {
        Task<Curso?> GetByIdAsync(int id);
        Task AddAsync(Curso curso);
        Task<List<Curso>> GetAllAsync();
    }

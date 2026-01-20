using Microsoft.EntityFrameworkCore;
using Portal_Aluno.Domain.Entities;

namespace Portal_Aluno.Infrastructure.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Disciplina> Disciplinas => Set<Disciplina>();
    public DbSet<CursoDisciplina> CursosDisciplinas => Set<CursoDisciplina>();
    public DbSet<Professor> Professores => Set<Professor>();
    public DbSet<Matricula> Matriculas => Set<Matricula>();
    public DbSet<Turma> Turmas => Set<Turma>();
    public DbSet<MatriculaTurma> MatriculasTurma => Set<MatriculaTurma>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(b =>
        {
            b.HasKey(x => x.Ra);
        });

        modelBuilder.Entity<MatriculaTurma>(b =>
        {
            b.Property(x => x.Nota).HasPrecision(4, 2);
        });

        modelBuilder.Entity<Usuario>(b =>
        {
            b.HasIndex(x => x.Login).IsUnique();
        });
    }
}



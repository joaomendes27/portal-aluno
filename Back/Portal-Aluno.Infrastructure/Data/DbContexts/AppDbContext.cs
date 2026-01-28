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
    public DbSet<Sala> Salas => Set<Sala>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(b =>
        {
            b.HasKey(x => x.Ra);
            b.Property(x => x.Ra).ValueGeneratedOnAdd();
            b.HasOne(a => a.Matricula)
                .WithOne(m => m.Aluno)
                .HasForeignKey<Matricula>(m => m.AlunoRa);
        });

        modelBuilder.Entity<Professor>(b =>
        {
            b.HasMany(p => p.Turmas)
                .WithOne(t => t.Professor)
                .HasForeignKey(t => t.ProfessorId);
        });

        modelBuilder.Entity<Turma>(b =>
        {
            b.HasOne(t => t.Sala)
                .WithMany()
                .HasForeignKey(t => t.SalaId);
        });

        modelBuilder.Entity<Sala>(b =>
        {
            b.ToTable("salas");
        });

        modelBuilder.Entity<Curso>(b =>
        {
            b.HasMany(c => c.Matriculas)
                .WithOne(m => m.Curso)
                .HasForeignKey(m => m.CursoId);

            b.HasMany(c => c.Turmas)
                .WithOne(t => t.Curso)
                .HasForeignKey(t => t.CursoId);
        });

        modelBuilder.Entity<Disciplina>(b =>
        {
            b.HasMany(d => d.Turmas)
                .WithOne(t => t.Disciplina)
                .HasForeignKey(t => t.DisciplinaId);
        });

        modelBuilder.Entity<CursoDisciplina>(b =>
        {
            b.ToTable("curso_disciplina");
            b.HasKey(cd => new { cd.CursoId, cd.DisciplinaId });

            b.HasOne(cd => cd.Curso)
                .WithMany(c => c.CursoDisciplinas)
                .HasForeignKey(cd => cd.CursoId);

            b.HasOne(cd => cd.Disciplina)
                .WithMany(d => d.CursoDisciplinas)
                .HasForeignKey(cd => cd.DisciplinaId);
        });

        modelBuilder.Entity<MatriculaTurma>(b =>
        {
            b.ToTable("matricula_turma");
            b.HasKey(mt => new { mt.MatriculaId, mt.TurmaId });
            b.Property(x => x.Nota).HasPrecision(4, 2);

            b.HasOne(mt => mt.Matricula)
                .WithMany(m => m.MatriculaTurmas)
                .HasForeignKey(mt => mt.MatriculaId);

            b.HasOne(mt => mt.Turma)
                .WithMany(t => t.MatriculaTurmas)
                .HasForeignKey(mt => mt.TurmaId);
        });

        modelBuilder.Entity<Usuario>(b =>
        {
            b.HasIndex(x => x.Login).IsUnique();

            b.Ignore(x => x.Aluno);
            b.Ignore(x => x.Professor);
        });
    }
}








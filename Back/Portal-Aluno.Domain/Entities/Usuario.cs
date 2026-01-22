namespace Portal_Aluno.Domain.Entities;

    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!; 
        public string Senha { get; set; } = null!;
        public string Tipo { get; set; } = null!;  
        public string? ReferenciaId { get; set; } 

        public Aluno? Aluno { get; set; }
        public Professor? Professor { get; set; }
    }
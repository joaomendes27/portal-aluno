namespace Portal_Aluno.Domain.Entities;

    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty; 
        public string Senha { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;  
        public string? ReferenciaId { get; set; } 
        public string Email { get; set; } = string.Empty;

        public Aluno? Aluno { get; set; }
        public Professor? Professor { get; set; }
    }
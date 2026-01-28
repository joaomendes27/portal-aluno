namespace Portal_Aluno.Domain.Entities;

public class Sala
{
    public int Id { get; set; }
    public int? Andar { get; set; }
    public string Numero { get; set; } = string.Empty;

    public List<HorarioAula> HorariosAula { get; set; } = new();
}

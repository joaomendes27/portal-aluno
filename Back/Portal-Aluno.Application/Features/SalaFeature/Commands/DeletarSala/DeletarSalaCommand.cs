using MediatR;

public class DeletarSalaCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public DeletarSalaCommand(int id)
    {
        Id = id;
    }
}
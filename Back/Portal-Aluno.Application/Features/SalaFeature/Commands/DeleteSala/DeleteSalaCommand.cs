using MediatR;

public class DeleteSalaCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public DeleteSalaCommand(int id)
    {
        Id = id;
    }
}
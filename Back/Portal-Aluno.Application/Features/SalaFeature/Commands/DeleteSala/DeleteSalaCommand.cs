using MediatR;

namespace Features.SalaFeature.Commands.DeleteSala;

public class DeleteSalaCommand : IRequest
{
    public int Id { get; }

    public DeleteSalaCommand(int id)
    {
        Id = id;
    }
}

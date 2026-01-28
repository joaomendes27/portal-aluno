using MediatR;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;

namespace Features.SalaFeature.Commands.UpdateSala;

public class UpdateSalaCommand : IRequest<SalaResponse>
{
    public int Id { get; }
    public SalaRequest Dto { get; }

    public UpdateSalaCommand(int id, SalaRequest dto)
    {
        Id = id;
        Dto = dto;
    }
}

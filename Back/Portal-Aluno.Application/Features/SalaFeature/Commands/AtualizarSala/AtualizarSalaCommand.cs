using MediatR;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;

namespace Portal_Aluno.Application.Features.SalaFeature.Commands.UpdateSala;

public class AtualizarSalaCommand : IRequest<SalaResponse>
{
    public int Id { get; }
    public SalaRequest Dto { get; }

    public AtualizarSalaCommand(int id, SalaRequest dto)
    {
        Id = id;
        Dto = dto;
    }
}

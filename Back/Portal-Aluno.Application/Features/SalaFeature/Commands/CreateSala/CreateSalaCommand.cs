using MediatR;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;

namespace Portal_Aluno.Application.Features.SalaFeature.Commands.CreateSala;

public class CreateSalaCommand : IRequest<SalaResponse>
{
    public SalaRequest Dto { get; }
    public CreateSalaCommand(SalaRequest dto) => Dto = dto;
}

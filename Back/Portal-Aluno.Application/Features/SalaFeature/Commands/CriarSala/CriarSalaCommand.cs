using MediatR;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;

namespace Portal_Aluno.Application.Features.SalaFeature.Commands.CreateSala;

public class CriarSalaCommand : IRequest<SalaResponse>
{
    public SalaRequest Dto { get; }
    public CriarSalaCommand(SalaRequest dto) => Dto = dto;
}

using MediatR;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;

namespace Portal_Aluno.Application.Features.SalaFeature.Queries;

public class GetAllSalasQueryHandler : IRequestHandler<GetAllSalasQuery, List<SalaResponse>>
{
    private readonly ISalaRepository _salaRepository;

    public GetAllSalasQueryHandler(ISalaRepository salaRepository)
    {
        _salaRepository = salaRepository;
    }

    public async Task<List<SalaResponse>> Handle(GetAllSalasQuery request, CancellationToken cancellationToken)
    {
        var salas = await _salaRepository.GetAllAsync();
        
        return salas.Select(s => new SalaResponse(s.Id, s.Andar, s.Numero)).ToList();
    }
}

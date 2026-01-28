using MediatR;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;

namespace Portal_Aluno.Application.Features.SalaFeature.Queries;

public class GetSalaByIdQueryHandler : IRequestHandler<GetSalaByIdQuery, SalaResponse>
{
    private readonly ISalaRepository _salaRepository;

    public GetSalaByIdQueryHandler(ISalaRepository salaRepository)
    {
        _salaRepository = salaRepository;
    }

    public async Task<SalaResponse> Handle(GetSalaByIdQuery request, CancellationToken cancellationToken)
    {
        var sala = await _salaRepository.GetByIdAsync(request.Id);
        if (sala == null)
        {
            throw new Exception("Sala não encontrada."); // Idealmente, uma exceção customizada
        }

        return new SalaResponse(sala.Id, sala.Andar, sala.Numero);
    }
}

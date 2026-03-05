using MediatR;
using Portal_Aluno.Domain.Entities;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;

namespace Portal_Aluno.Application.Features.SalaFeature.Commands.CreateSala;

public class CriarSalaCommandHandler : IRequestHandler<CriarSalaCommand, SalaResponse>
{
    private readonly ISalaRepository _salaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CriarSalaCommandHandler(ISalaRepository salaRepository, IUnitOfWork unitOfWork)
    {
        _salaRepository = salaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SalaResponse> Handle(CriarSalaCommand request, CancellationToken cancellationToken)
    {
        var sala = new Sala
        {
            Andar = request.Dto.Andar,
            Numero = request.Dto.Numero
        };

        await _salaRepository.AddAsync(sala);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new SalaResponse(sala.Id, sala.Andar, sala.Numero);
    }
}

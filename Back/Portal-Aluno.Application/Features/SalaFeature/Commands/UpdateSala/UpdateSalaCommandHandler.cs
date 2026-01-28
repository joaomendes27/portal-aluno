using MediatR;
using Portal_Aluno.Domain.Interfaces;
using Portal_Aluno.Application.Features.SalaFeature.DTOs;

namespace Portal_Aluno.Application.Features.SalaFeature.Commands;

public class UpdateSalaCommandHandler : IRequestHandler<UpdateSalaCommand, SalaResponse>
{
    private readonly ISalaRepository _salaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSalaCommandHandler(ISalaRepository salaRepository, IUnitOfWork unitOfWork)
    {
        _salaRepository = salaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SalaResponse> Handle(UpdateSalaCommand request, CancellationToken cancellationToken)
    {
        var sala = await _salaRepository.GetByIdAsync(request.Id);
        if (sala == null)
        {
            throw new Exception("Sala não encontrada."); // Idealmente, uma exceção customizada
        }

        sala.Andar = request.Dto.Andar;
        sala.Numero = request.Dto.Numero;

        _salaRepository.Update(sala);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new SalaResponse(sala.Id, sala.Andar, sala.Numero);
    }
}

using MediatR;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Features.SalaFeature.Commands.DeleteSala;



public class DeleteSalaCommandHandler : IRequestHandler<DeleteSalaCommand, Unit>
{
	private readonly ISalaRepository _salaRepository;
	private readonly IUnitOfWork _unitOfWork;

	public DeleteSalaCommandHandler(ISalaRepository salaRepository, IUnitOfWork unitOfWork)
	{
		_salaRepository = salaRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeleteSalaCommand request, CancellationToken cancellationToken)
	{
		var sala = await _salaRepository.GetByIdAsync(request.Id);
		if (sala != null)
		{
			_salaRepository.Delete(sala);
			await _unitOfWork.SaveChangesAsync(cancellationToken);
		}
	

		return Unit.Value;
	}
}
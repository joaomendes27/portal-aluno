using MediatR;
using Portal_Aluno.Application.Features.CursoFeature.DTOs;

namespace Portal_Aluno.Application.Features.CursoFeature.Commands.AtualizarCurso;

public class AtualizarCursoCommand : IRequest<Unit>
{
    public int Id { get; }
    public AtualizarCursoRequest Dto { get; }

    public AtualizarCursoCommand(int id, AtualizarCursoRequest dto)
    {
        Id = id;
        Dto = dto;
    }
}

using MediatR;
using Portal_Aluno.Application.Features.CursoFeature.DTOs;

namespace Portal_Aluno.Application.Features.CursoFeature.Commands.RemoverDisciplinaDoCurso;

public class RemoverDisciplinaDoCursoCommand : IRequest<Unit>
{
    public RemoverDisciplinaDoCursoRequest Dto { get; }

    public RemoverDisciplinaDoCursoCommand(RemoverDisciplinaDoCursoRequest dto)
    {
        Dto = dto;
    }
}

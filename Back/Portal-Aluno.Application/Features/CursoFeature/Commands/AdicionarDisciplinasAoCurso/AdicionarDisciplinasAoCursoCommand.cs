using MediatR;
using Portal_Aluno.Application.Features.CursoFeature.DTOs;

namespace Portal_Aluno.Application.Features.CursoFeature.Commands.AdicionarDisciplinasAoCurso;

public class AdicionarDisciplinasAoCursoCommand : IRequest<Unit>
{
    public AdicionarDisciplinasAoCursoRequest Dto { get; }

    public AdicionarDisciplinasAoCursoCommand(AdicionarDisciplinasAoCursoRequest dto)
    {
        Dto = dto;
    }
}

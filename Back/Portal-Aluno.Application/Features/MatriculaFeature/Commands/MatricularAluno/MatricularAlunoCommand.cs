using MediatR;
using Portal_Aluno.Application.Features.MatriculaFeature.DTOs;

namespace Portal_Aluno.Application.Features.MatriculaFeature.Commands.MatricularAluno;

public class MatricularAlunoCommand : IRequest<int>
{
    public MatricularAlunoRequest Dto { get; }

    public MatricularAlunoCommand(MatricularAlunoRequest dto)
    {
        Dto = dto;
    }
}

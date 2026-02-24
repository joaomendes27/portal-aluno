using MediatR;
using Portal_Aluno.Application.Features.DisciplinaFeature.DTOs;

namespace Portal_Aluno.Application.Features.DisciplinaFeature.Commands.CadastrarDisciplinas;

public class CadastrarDisciplinasCommand : IRequest<List<int>>
{
    public CadastrarDisciplinasRequest Request { get; }

    public CadastrarDisciplinasCommand(CadastrarDisciplinasRequest request)
    {
        Request = request;
    }
}

using crud2_webapi.Dto;
using crud2_webapi.Models;

namespace crud2_webapi.Services
{
    public interface IAlunoInterface
    {
        Task<ResponseModel<List<AlunoListarDto>>> BuscarAlunos();

        Task<ResponseModel<AlunoListarDto>> BuscarAlunoPorId(int alunoId);

        Task<ResponseModel<List<AlunoListarDto>>> AdicionarAluno(AlunoCriarDto aluno);

        Task<ResponseModel<List<AlunoListarDto>>> EditarAluno(AlunoEditarDto aluno);

        Task<ResponseModel<List<AlunoListarDto>>> DeletarAluno(int alunoId);
    }
}

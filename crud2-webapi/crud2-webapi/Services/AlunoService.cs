using AutoMapper;
using crud2_webapi.Dto;
using crud2_webapi.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Data.SqlClient;

namespace crud2_webapi.Services
{
    public class AlunoService : IAlunoInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AlunoService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        private static async Task<IEnumerable<Aluno>> ListarAlunos(SqlConnection connection)
        {
            return await connection.QueryAsync<Aluno>("select * from Alunos");
        }

        public async Task<ResponseModel<List<AlunoListarDto>>> AdicionarAluno(AlunoCriarDto aluno)
        {
            ResponseModel<List<AlunoListarDto>> resposta = new ResponseModel<List<AlunoListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DB")))
            {
                var alunoBanco = await connection.ExecuteAsync("insert into Alunos (Nome, Idade, Matricula)" +
                                                               "values (@Nome, @Idade, @Matricula)", aluno);

                if(alunoBanco == 0)
                {
                    resposta.Mensagem = "Ocorreu um erro ao adicionar o aluno";
                    resposta.Status = false;
                    return resposta;
                }

                var alunos = await ListarAlunos(connection);

                var alunosMapeados = _mapper.Map<List<AlunoListarDto>>(alunos);

                resposta.Dados = alunosMapeados;
                resposta.Mensagem = "Alunos listados com sucesso!";
            }

            return resposta;

        }

        public async Task<ResponseModel<AlunoListarDto>> BuscarAlunoPorId(int alunoId)
        {
            ResponseModel<AlunoListarDto> resposta = new ResponseModel<AlunoListarDto>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DB")))
            {
                var alunoBanco = await connection.QueryFirstOrDefaultAsync<Aluno>("select * from Alunos where id= @Id ", new { Id = alunoId });

                if(alunoBanco == null)
                {
                    resposta.Mensagem = "Nenhum aluno encontrado!";
                    resposta.Status = false;
                    return resposta;
                }

                var alunoMapeado = _mapper.Map<AlunoListarDto>(alunoBanco);

                resposta.Dados = alunoMapeado;
                resposta.Mensagem = "Aluno encontrado com sucesso";

            }

            return resposta;

        }

        public async Task<ResponseModel<List<AlunoListarDto>>> BuscarAlunos()
        {
            ResponseModel<List<AlunoListarDto>> resposta = new ResponseModel<List<AlunoListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DB")))
            {
                var alunosBanco = await connection.QueryAsync<Aluno>("select * from Alunos");

                if(alunosBanco.Count() == 0)
                {
                    resposta.Mensagem = "Nenhum aluno encontrado!";
                    resposta.Status = false;
                    return resposta;
                }

                var alunosMapeado = _mapper.Map<List<AlunoListarDto>>(alunosBanco);

                resposta.Dados = alunosMapeado;
                resposta.Mensagem = "Usuários localizados!";

            }

            return resposta;

        }

        public async Task<ResponseModel<List<AlunoListarDto>>> DeletarAluno(int alunoId)
        {
            ResponseModel<List<AlunoListarDto>> resposta = new ResponseModel<List<AlunoListarDto>> ();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DB")))
            {
                var aluno = await connection.ExecuteAsync("delete from Alunos where Id = @Id", new {Id=  alunoId });

                if(aluno == 0)
                {
                    resposta.Mensagem = "erro ao deletar";
                    resposta.Status = false;
                }

                var alunoslista = await ListarAlunos(connection);

                var alunoslistamapeado = _mapper.Map<List<AlunoListarDto>>(alunoslista);

                resposta.Dados = alunoslistamapeado;
                resposta.Mensagem = "Remoção realizada com sucesso";

                
            }

            return resposta;
        }

        public async Task<ResponseModel<List<AlunoListarDto>>> EditarAluno(AlunoEditarDto a)
        {
            ResponseModel<List<AlunoListarDto>> resposta = new ResponseModel<List<AlunoListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DB")))
            {
                var alunoeditado = await connection.ExecuteAsync("update Alunos set  Nome = @Nome, Idade = @Idade, Matricula = @Matricula where Id = @Id", a);

                if(alunoeditado == 0)
                {
                    resposta.Mensagem = "erro durante a edição";
                    resposta.Status = false;
                    return resposta;
                }

                var alunos = await ListarAlunos(connection);

                var alunosmapeados = _mapper.Map<List<AlunoListarDto>>(alunos);

                resposta.Dados = alunosmapeados;
                resposta.Mensagem = "Aluno editado com sucesso";

            }

            return resposta;

        }
    }
}

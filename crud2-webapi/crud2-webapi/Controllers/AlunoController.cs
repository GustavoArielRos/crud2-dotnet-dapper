using crud2_webapi.Dto;
using crud2_webapi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crud2_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoInterface _alunoInterface;


        public AlunoController(IAlunoInterface alunoInterface)
        {
            _alunoInterface = alunoInterface; 
        }

        [HttpGet]
        public async Task<IActionResult> BuscarAlunos()
        {
            var alunos = await _alunoInterface.BuscarAlunos();

            if(alunos.Status == false)
            {
                return NotFound(alunos);
            }

            return Ok(alunos);
        }

        [HttpGet("{alunoId}")]
        public async Task<IActionResult> BuscarAlunoId(int alunoId)
        {
            var aluno = await _alunoInterface.BuscarAlunoPorId(alunoId);

            if(aluno.Status == false)
            {
                return NotFound(aluno);
            }

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<IActionResult> CriarAluno(AlunoCriarDto a)
        {
            var alunos = await _alunoInterface.AdicionarAluno(a);

            if(alunos.Status == false)
            {
                return BadRequest(alunos);
            }

            return Ok(alunos);
        }

        [HttpPut]

        public async Task<IActionResult> AlterandoAluno(AlunoEditarDto a)
        {
            var alunos =  await _alunoInterface.EditarAluno(a);

            if(alunos.Status = false)
            {
                return BadRequest(alunos);
            }

            return Ok(alunos);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarAluno(int alunoid)
        {
            var alunos = await _alunoInterface.DeletarAluno(alunoid);

            if(alunos.Status == false)
            {
                return BadRequest(alunos);
            }

            return Ok(alunos);
        }
    }
}

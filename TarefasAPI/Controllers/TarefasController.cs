using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarefasApi.Repositorios.Interfaces;
using TarefasAPI.Models;

namespace TarefasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefasRepository _tarefasRepository;

        public TarefasController(ITarefasRepository tarefasRepository)
        {
            _tarefasRepository = tarefasRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tarefa>>> BuscarTodasTarefas()
        {
            try
            {
                List<Tarefa> tarefas = await _tarefasRepository.BuscarTodasTarefas();

                return Ok(tarefas);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno no servidor." });
            }
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Tarefa>>> BuscarTarefasUsuarioId(int id)
        {
            try
            {
                List<Tarefa> tarefas = await _tarefasRepository.BuscarTarefasUsuario(id);
                return Ok(tarefas);
            }
            catch(KeyNotFoundException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno no servidor."});
            }
            
        }

        [HttpGet("{idUsuario}/{idTarefa}")]
        public async Task<ActionResult<Tarefa>> BuscarTarefaUsuarioId(int idUsuario, int idTarefa)
        {
            try
            {
                Tarefa tarefa = await _tarefasRepository.BuscarTarefaUsuarioId(idUsuario, idTarefa);

                return Ok(tarefa);
            }
            catch(KeyNotFoundException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno no servidor." });
            }
            
        }

        [HttpPost("{idUsuario}")]
        public async Task<ActionResult<Tarefa>> CadastrarTarefa([FromBody] Tarefa tarefa, int idUsuario)
        {
            try
            {
                Tarefa tarefaCadastrada = await _tarefasRepository.CadastrarTarefa(tarefa, idUsuario);
                return Ok(tarefaCadastrada);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno no servidor." });
            }

        }

        [HttpPut("{idUsuario}/{idTarefa}")]
        public async Task<ActionResult<Tarefa>> AtualizarTarefa([FromBody] Tarefa tarefa, int idUsuario, int idTarefa)
        {
            try
            {
                Tarefa tarefaAtt = await _tarefasRepository.AtualizarTarefa(tarefa, idTarefa, idUsuario);

                return Ok(tarefaAtt);
            }
            catch(KeyNotFoundException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno no servidor." });
            }

        }

        [HttpDelete("{idUsuario}/{idTarefa}")]
        public async Task<ActionResult<Tarefa>> ExcluirTarefa(int idTarefa, int idUsuario)
        {
            try
            {
                Tarefa tarefaDelete = await _tarefasRepository.ExcluirTarefa(idTarefa, idUsuario);

                return Ok(tarefaDelete);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno no servidor." });
            }
        }
    }
}

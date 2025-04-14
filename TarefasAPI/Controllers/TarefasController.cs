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

        [HttpGet("get")]
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

        [HttpGet("get_tarefa")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<List<Tarefa>>> BuscarTarefasUsuarioId([FromQuery] int id)
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

        [HttpGet("get_tarefa_usuario")]
        //[HttpGet("{idUsuario}/{idTarefa}")]
        public async Task<ActionResult<Tarefa>> BuscarTarefaUsuarioId([FromQuery] int idUsuario, [FromQuery] int idTarefa)
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

        [HttpPost("add")]
        //[HttpPost("{idUsuario}")]
        public async Task<ActionResult<Tarefa>> CadastrarTarefa([FromBody] Tarefa tarefa, [FromQuery] int idUsuario)
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

        [HttpPut("Atualizar")]
        //[HttpPut("{idUsuario}/{idTarefa}")]
        public async Task<ActionResult<Tarefa>> AtualizarTarefa([FromBody] Tarefa tarefa, [FromQuery] int idUsuario, [FromQuery] int idTarefa)
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

        [HttpDelete("delete")]
        //[HttpDelete("{idUsuario}/{idTarefa}")]
        public async Task<ActionResult<Tarefa>> ExcluirTarefa([FromQuery] int idTarefa, [FromQuery] int idUsuario)
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

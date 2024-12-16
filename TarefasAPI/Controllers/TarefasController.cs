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
            List<Tarefa> tarefas = await _tarefasRepository.BuscarTodasTarefas();

            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Tarefa>>> BuscarTarefasUsuarioId(int id)
        {
            List<Tarefa> tarefas = await _tarefasRepository.BuscarTarefasUsuario(id);
            return Ok(tarefas);
        }

        [HttpGet("{idUsuario}/{idTarefa}")]
        public async Task<ActionResult<Tarefa>> BuscarTarefaUsuarioId(int idUsuario, int idTarefa)
        {
            Tarefa tarefa = await _tarefasRepository.BuscarTarefaUsuarioId(idUsuario, idTarefa);

            return Ok(tarefa);
        }

        [HttpPost("{idUsuario}")]
        public async Task<ActionResult<Tarefa>> CadastrarTarefa([FromBody] Tarefa tarefa, int idUsuario)
        {
            Tarefa tarefaCadastrada = await _tarefasRepository.CadastrarTarefa(tarefa, idUsuario);

            return Ok(tarefaCadastrada);
        }

        [HttpPut("{idUsuario}/{idTarefa}")]
        public async Task<ActionResult<Tarefa>> AtualizarTarefa([FromBody] Tarefa tarefa, int idUsuario, int idTarefa)
        {
            Tarefa tarefaAtt = await _tarefasRepository.AtualizarTarefa(tarefa, idTarefa, idUsuario);

            return Ok(tarefaAtt);
        }

        [HttpDelete("{idUsuario}/{idTarefa}")]
        public async Task<ActionResult<Tarefa>> ExcluirTarefa(int idTarefa, int idUsuario)
        {
            Tarefa tarefaDelete = await _tarefasRepository.ExcluirTarefa(idTarefa, idUsuario);

            return Ok(tarefaDelete);
        }
    }
}

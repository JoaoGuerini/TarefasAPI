using Microsoft.AspNetCore.Mvc;
using TarefasApi.Repositorios;
using TarefasApi.Repositorios.Interfaces;
using TarefasAPI.Models;

namespace TarefasApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubtaskController : ControllerBase
{
    private readonly ISubtasksRepository _subtasksRepository;
    public SubtaskController(ISubtasksRepository subtasksRepository)
    {
        _subtasksRepository = subtasksRepository;
    }

    [HttpGet("get")]
    public async Task<ActionResult<List<SubTarefa>>> BuscarSubtarefas([FromQuery] int idTarefa)
    {
        try
        {
            List<SubTarefa> listaSubtarefa = await _subtasksRepository.BuscarTarefas(idTarefa);
            return Ok(listaSubtarefa);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Erro interno no servidor" });
        }

    }
}

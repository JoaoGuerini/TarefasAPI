using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarefasApi.Repositorios.Interfaces;
using TarefasAPI.Models;

namespace TarefasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        [HttpGet("get")]
        public async Task<ActionResult<List<Usuario>>> BuscarUsuarios()
        {
            try
            {
                List<Usuario> listaUsuarios = await _usuarioRepository.BuscarTodosUsuarios();
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno no servidor" });
            }
            
        }

        [HttpGet("get_id")]
        public async Task<ActionResult<Usuario>> BuscarPorId([FromQuery] int id)
        {
            try
            {
                Usuario usuario = await _usuarioRepository.BuscarId(id);
                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno no servidor" });
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<Usuario>> Cadastrar([FromBody] Usuario usuario)
        {
            try
            {
                Usuario usuarioAdd = await _usuarioRepository.Adicionar(usuario);

                return Ok(usuarioAdd);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno no servidor" });
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Usuario>> RemoverTodos([FromQuery] int id)
        {
            try
            {
                bool excluido = await _usuarioRepository.Apagar(id);
                return Ok(excluido);
            }
            catch(KeyNotFoundException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno no servidor." });
            }
            
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult<Usuario>> Atualizar([FromBody] Usuario usuario, [FromQuery] int id)
        {
            try
            {
                usuario.Id = id;
                Usuario usuarioAtt = await _usuarioRepository.Atualizar(usuario, id);
                return Ok(usuarioAtt);
            }
            catch(KeyNotFoundException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno no servidor" });
            }
            
        }

    }
}

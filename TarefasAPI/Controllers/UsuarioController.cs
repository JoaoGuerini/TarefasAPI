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
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> BuscarUsuarios()
        {
            List<Usuario> listaUsuarios = await _usuarioRepository.BuscarTodosUsuarios();
            return Ok(listaUsuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> BuscarPorId(int id)
        {
            Usuario usuario = await _usuarioRepository.BuscarId(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Cadastrar([FromBody] Usuario usuario)
        {
            Usuario usuarioAdd = await _usuarioRepository.Adicionar(usuario);

            return Ok(usuarioAdd);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> RemoverTodos(int id)
        {
            var excluido = await _usuarioRepository.Apagar(id);
            return Ok(excluido);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Atualizar([FromBody] Usuario usuario, int id)
        {
            usuario.Id = id;
            Usuario usuarioAtt = await _usuarioRepository.Atualizar(usuario, id);
            return Ok(usuarioAtt);
        }

    }
}

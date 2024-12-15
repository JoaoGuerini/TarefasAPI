using TarefasAPI.Models;

namespace TarefasApi.Repositorios.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> BuscarTodosUsuarios();
        Task<Usuario> BuscarId(int id);
        Task<Usuario> Adicionar(Usuario usuario);
        Task<Usuario> Atualizar(Usuario usuario, int id);
        Task<bool> Apagar(int id);
    }
}

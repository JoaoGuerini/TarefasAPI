using TarefasAPI.Models;

namespace TarefasApi.Repositorios.Interfaces;

public interface ITarefasRepository
{
    Task<List<Tarefa>> BuscarTodasTarefas();
    Task<Tarefa> BuscarTarefaId(int idTarefa);
    Task<List<Tarefa>> BuscarTarefasUsuario(int idUsuario);
    Task<Tarefa> BuscarTarefaUsuarioId(int idUsuario, int idTarefa);
    Task<Tarefa> CadastrarTarefa(Tarefa tarefa, int idUsuario);
    Task<Tarefa> AtualizarTarefa(Tarefa tarefa, int idTarefa, int idUsuario);
    Task<Tarefa> ExcluirTarefa(int idTarefa, int idUsuario);
}

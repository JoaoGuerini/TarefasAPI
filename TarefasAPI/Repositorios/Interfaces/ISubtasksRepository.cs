using TarefasAPI.Models;

namespace TarefasApi.Repositorios.Interfaces;

public interface ISubtasksRepository
{
    Task<List<SubTarefa>> BuscarTarefas(int idTarefa);
}

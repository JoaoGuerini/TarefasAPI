using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Repositorios.Interfaces;
using TarefasAPI.Models;

namespace TarefasApi.Repositorios;

public class SubtasksRepository : ISubtasksRepository
{
    private readonly TarefasDBContext _dbContext;
    private readonly ITarefasRepository _tarefasRepository;
    public SubtasksRepository(
        TarefasDBContext dbContext,
        ITarefasRepository tarefasRepository)
    {
        _dbContext = dbContext;
        _tarefasRepository = tarefasRepository;
    }
    public async Task<List<SubTarefa>> BuscarTarefas(int idTarefa)
    {
        List<Tarefa> tarefas = await _tarefasRepository.BuscarTodasTarefas();
        Tarefa tarefa = tarefas.FirstOrDefault(x => x.Id == idTarefa);

        if (tarefa == null)
        {
            throw new KeyNotFoundException($"Erro Usuario: Tarefa de id {idTarefa} não encontrada");
        }

        return await _dbContext.SubTarefas
            .Where(subtarefa => subtarefa.TarefaId == idTarefa)
            .ToListAsync();
    }
}

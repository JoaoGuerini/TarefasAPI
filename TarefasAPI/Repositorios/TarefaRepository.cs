using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Repositorios.Interfaces;
using TarefasAPI.Models;

namespace TarefasApi.Repositorios
{
    public class TarefaRepository : ITarefasRepository
    {
        private readonly TarefasDBContext _dbContext;
        private readonly IUsuarioRepository _usuarioRepository;

        public TarefaRepository(IUsuarioRepository usuarioRepository, TarefasDBContext tarefasDBContext)
        {
            _dbContext = tarefasDBContext;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<List<Tarefa>> BuscarTarefasUsuario(int idUsuario)
        {
            Usuario usuarioId = await _usuarioRepository.BuscarId(idUsuario);

            if (usuarioId == null)
            {
                throw new KeyNotFoundException($"Erro Usuario: Usuario de id {usuarioId} não encontrado");
            }

            return await _dbContext.Tarefas
                .Where(tarefa => tarefa.UsuarioId == idUsuario)
                .ToListAsync();
        }

        public async Task<Tarefa> BuscarTarefaUsuarioId(int idUsuario, int idTarefa)
        {
            Usuario usuarioId = await _usuarioRepository.BuscarId(idUsuario);

            if (usuarioId == null)
            {
                throw new KeyNotFoundException($"Erro Usuario: Usuario de id {usuarioId} não encontrado");
            }

            return await _dbContext.Tarefas.FirstOrDefaultAsync(tarefa => tarefa.Id == idTarefa && tarefa.UsuarioId == idUsuario);
        }

        public async Task<List<Tarefa>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas.ToListAsync();
        }
        public async Task<Tarefa> AtualizarTarefa(Tarefa tarefa, int idTarefa, int idUsuario)
        {
            Usuario usuarioId = await _usuarioRepository.BuscarId(idUsuario);

            if (usuarioId == null)
            {
                throw new KeyNotFoundException($"Erro Usuario: Usuario de id {usuarioId} não encontrado");
            }

            Tarefa tarefaId = await BuscarTarefaUsuarioId(idUsuario, idTarefa);

            if(tarefaId == null)
            {
                throw new KeyNotFoundException($"Erro tarefa: {tarefaId} não encontrada");
            }

            tarefaId.Name = tarefa.Name;
            tarefaId.Status = tarefa.Status;
            tarefaId.Usuario = tarefaId.Usuario;
            tarefaId.UsuarioId = tarefaId.UsuarioId;
            tarefaId.Description = tarefa.Description;

            _dbContext.Tarefas.Update(tarefaId);
            await _dbContext.SaveChangesAsync();

            return tarefaId;
        }

        public async Task<Tarefa> CadastrarTarefa(Tarefa tarefa, int idUsuario)
        {

            Usuario usuarioId = await _usuarioRepository.BuscarId(idUsuario);

            if(usuarioId == null)
            {
                throw new KeyNotFoundException($"Erro Usuario: Usuario de id {usuarioId} não encontrado");
            }

            tarefa.UsuarioId = idUsuario;

            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<Tarefa> ExcluirTarefa(int idTarefa, int idUsuario)
        {
            Usuario usuarioId = await _usuarioRepository.BuscarId(idUsuario);
            Tarefa tarefaId = await BuscarTarefaUsuarioId(idUsuario, idTarefa);

            if (usuarioId == null)
            {
                throw new KeyNotFoundException($"Erro Usuario: Usuario de id {usuarioId} não encontrado");
            }

            if (tarefaId == null)
            {
                throw new KeyNotFoundException($"Erro tarefa: {tarefaId} não encontrada");
            }

            _dbContext.Remove(tarefaId);
            await _dbContext.SaveChangesAsync();

            return tarefaId;
        }
    }
}

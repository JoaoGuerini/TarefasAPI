
using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Repositorios.Interfaces;
using TarefasAPI.Models;

namespace TarefasApi.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private TarefasDBContext _dbContext;
        public UsuarioRepository(TarefasDBContext tarefasDBContext)
        {
            _dbContext = tarefasDBContext;
        }
        public async Task<Usuario> BuscarId(int id)
        {
            var usuarioId = await _dbContext.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);
             
            if(usuarioId == null)
            {
                throw new KeyNotFoundException($"Usuario por ID: Usuario não encontrado");
            }

            return usuarioId;
        }

        public async Task<List<Usuario>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();

        }
        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            try
            {
                await _dbContext.Usuarios.AddAsync(usuario);
                await _dbContext.SaveChangesAsync();

                return usuario;
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException($"Informações incorretas");
            }
            
        }

        public async Task<Usuario> Atualizar(Usuario usuario, int idUsuario)
        {
            Usuario usuarioId = await BuscarId(idUsuario);
            if (usuarioId == null)
            {
                throw new KeyNotFoundException($"Usuario por ID: {idUsuario} não encontrado");
            }

            usuarioId.Name = usuario.Name;
            usuarioId.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioId);
            await _dbContext.SaveChangesAsync();
            return usuarioId;

        }

        public async Task<bool> Apagar(int idUsuario)
        {
            Usuario usuarioId = await BuscarId(idUsuario);

            if(usuarioId == null)
            {
                throw new KeyNotFoundException($"Apagar usuario por ID: {usuarioId} não encontrado");
            }
            var tarefasUser = _dbContext.Tarefas.Where(tar => tar.UsuarioId == usuarioId.Id);
            _dbContext.Tarefas.RemoveRange(tarefasUser);

            _dbContext.Usuarios.Remove(usuarioId);
            await _dbContext.SaveChangesAsync();
            return true; 
        }

        
        
    }
}

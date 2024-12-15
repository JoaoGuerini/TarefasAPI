
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
            return await _dbContext.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);
        }

        public async Task<List<Usuario>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();

        }
        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> Atualizar(Usuario usuario, int id)
        {
            Usuario usuarioId = await BuscarId(id);
            if (usuarioId == null)
            {
                throw new Exception($"Usuario por ID: {id} não encontrado");
            }

            usuarioId.Name = usuario.Name;
            usuarioId.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioId);
            await _dbContext.SaveChangesAsync();
            return usuarioId;

        }

        public async Task<bool> Apagar(int id)
        {
            Usuario usuarioId = await BuscarId(id);

            if(usuarioId == null)
            {
                throw new Exception($"Apagar usuario por ID: {id} não encontrado");
            }

            _dbContext.Usuarios.Remove(usuarioId);
            await _dbContext.SaveChangesAsync();
            return true; 
        }

        
        
    }
}

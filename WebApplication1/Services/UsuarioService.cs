using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UsuarioService
    {
        private List<Usuario> Usuarios { get; set; } = [];

        public Usuario Adicionar(Usuario usuario)
        {
            Usuarios.Add(usuario);
            return usuario;
        }
        public Usuario ObterPorId(string id)
        {
            if (id != null)
            {
                var users = Usuarios.FirstOrDefault(u => u.Id == id);
                return users;
            }
            else
            {
                return null;
            }
        }
        public Usuario Atualizar(string id, Usuario user)
        {
            if (id != null) 
            {
                var usuario = Usuarios.FirstOrDefault(u => u.Id == id);
                if (usuario != null)
                {
                    usuario.Nome = user.Nome;
                    usuario.Email = user.Email;
                    usuario.Senha = user.Senha;
                }
                return usuario;
            }
            else
            {
                return null;
            }
        }
        public void Deletar(string id)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                if (usuario == null)
                {
                    return;
                }
                Usuarios.Remove(usuario);
            }
        }
        public List<Usuario> ObterTodos()
        {
            return Usuarios;
        }
    }
}

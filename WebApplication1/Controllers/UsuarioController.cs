using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet("listar-todos")]
        [Authorize(Roles ="Admin, Usuario, Nilton")]
        public List<Usuario> Obtertodos()
        {
            var usuarios = _usuarioService.ObterTodos();
            return usuarios;
        }
        [HttpPost("cadastro")]
        public Usuario CadastrarUsuario ([FromBody]UsuarioRequest request)
        {
            var novoUsuario = _usuarioService.Adicionar(request);
            return novoUsuario;
        }
        [HttpGet("obter-por-id/{id}")]
        public Usuario ObterPorId(string id)
        {
            var usuario = _usuarioService.ObterPorId(id);
            return usuario;
        }
        [HttpPut("atualizar-cadastro")]
        public Usuario Atualizar(string id, [FromBody]UsuarioRequest request) 
        {
            var usuarioAtualizado = _usuarioService.Atualizar(id, request);
            return usuarioAtualizado;
        }
        [HttpDelete("deletar-usuario/{id}")]
        public void Deletar(string id)
        {
            _usuarioService.Deletar(id);
            _usuarioService.ObterTodos();
        }
    }
}

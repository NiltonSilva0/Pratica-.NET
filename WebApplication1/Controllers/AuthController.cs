using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLogin request)
        {
            // Exemplo de lista de usuários (substitua por acesso ao banco de dados)
            var usuarios = new List<(string Username, string Password, bool IsAdmin)>
            {
                ("Admin", "123456", true),
                ("Usuario", "123456", false),
                ("Nilton", "123456", true)
            };

            var usuario = usuarios.FirstOrDefault(u => 
                u.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == request.Password);

            if (usuario.Username != null)
            {
                if (usuario.IsAdmin)
                {
                    var token = GenerateJwtToken();
                    return Ok(new { Token = token });
                }
                else
                {
                    return Ok(new { Mensagem = "Login realizado com sucesso, porém sem privilégios de administrador.", Codigo = 0002 });
                }
            }

            return Unauthorized(new { Mensagem = "Credenciais Inválidas", Codigo = 0001 });
        }

        private string GenerateJwtToken()
        {
            var Claims = new[]
            {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim(ClaimTypes.Role, "Usuario"),
                new Claim(ClaimTypes.Role, "Nilton")
            };
            // Chave com 32 caracteres (256 bits)
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chaveSuperSecretaUltraSegura1234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "api-autenticacao",
                audience: "api-cadastro",
                claims: Claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("rota-protegida")]
        [Authorize]
        public IActionResult RotaProtegida()
        {
            return Ok(new { Mensagem = "Acesso autorizado a rota protegida." });
        }
    }
}
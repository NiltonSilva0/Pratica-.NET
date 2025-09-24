using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Usuario
    {
        [JsonIgnore]
        public Guid _Id { get; } = Guid.NewGuid();
        public string Id { get; } = Guid.NewGuid().ToString().Substring(0, 8);
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}

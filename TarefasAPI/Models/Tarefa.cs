using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;
using TarefasApi.Enums;

namespace TarefasAPI.Models
{
    public class Tarefa
    {
        [SwaggerIgnore]
        public int Id { get; set; }

        [SwaggerIgnore]
        public int? UsuarioId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public StatusTarefa Status { get; set; }

        public List<SubTarefa>? SubTarefa { get; set; }

        [JsonIgnore]
        public virtual Usuario? Usuario { get; set; }
    }

    public class SubTarefa 
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? TarefaId { get; set; }
    }

}

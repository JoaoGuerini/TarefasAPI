using Swashbuckle.AspNetCore.Annotations;

namespace TarefasAPI.Models
    {
        public class Usuario
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }

            [SwaggerIgnore]
            public List<Tarefa>? Tarefas { get; set; }
        }
    }   

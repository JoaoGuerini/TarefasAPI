using TarefasApi.Enums;

namespace TarefasAPI.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public StatusTarefa Status { get; set; }

    }
}
    
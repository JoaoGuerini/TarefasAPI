using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TarefasAPI.Models;

namespace TarefasApi.Data.Map
{
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).
                IsRequired().HasMaxLength(255)
                .HasColumnType("text");

            builder.Property(x => x.Description)
                .HasMaxLength(1000)
                .HasColumnType("text");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.UsuarioId)
                .HasColumnType("bigint");

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Tarefas)
                .HasForeignKey(x => x.UsuarioId);

            builder.HasMany(x => x.SubTarefa)
                .WithOne()
                .HasForeignKey(x => x.TarefaId);

        }

    }
    public class SubTarefaMap : IEntityTypeConfiguration<SubTarefa>
    {
        public void Configure(EntityTypeBuilder<SubTarefa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("text");

            builder.HasOne<Tarefa>()
               .WithMany(x => x.SubTarefa)
               .HasForeignKey(x => x.TarefaId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

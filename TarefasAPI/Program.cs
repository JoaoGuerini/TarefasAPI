
using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Repositorios;
using TarefasApi.Repositorios.Interfaces;

namespace TarefasApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TarefasDBContext>(
                    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                );

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<ITarefasRepository, TarefaRepository>();
            builder.Services.AddScoped<ISubtasksRepository, SubtasksRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

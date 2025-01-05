using System.Diagnostics;
using SQLiteMinecraftApi.Context;
using SQLiteMinecraftApi.Services;
using Microsoft.EntityFrameworkCore;

namespace SQLiteMinecraftApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.Services.AddDbContext<MinecraftContext>(options =>
                    options.UseSqlite(builder.Configuration.GetConnectionString("MinecraftDatabase")));
                builder.Services.AddScoped<IMinecraftServices, MinecraftServices>();
                builder.Services.AddMemoryCache(); // Add this line to enable in-memory caching

                builder.Services.AddControllers();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                if (builder.Configuration.GetValue<bool>("Swagger:Enabled"))
                {
                    builder.Services.AddEndpointsApiExplorer();
                    builder.Services.AddSwaggerGen();
                }

                var app = builder.Build();

                if (builder.Configuration.GetValue<bool>("Swagger:Enabled"))
                {
                    // Configure the HTTP request pipeline.
                    if (app.Environment.IsDevelopment())
                    {
                        app.UseSwagger();
                        app.UseSwaggerUI();
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://localhost:5000/swagger",
                            UseShellExecute = true
                        });
                    }
                }

                // Ensure the database is created and migrated
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<MinecraftContext>();
                    dbContext.Database.Migrate();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}

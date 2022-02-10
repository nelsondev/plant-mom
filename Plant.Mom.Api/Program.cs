using Plant.Mom.Api.Entities;

namespace Plant.Mom.Api;

public class Program
{
    public static void Main(string[] args)
    {
        // Configure services
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddDbContext<PlantContext>();
        builder.Services.AddMvc().AddNewtonsoftJson(x =>
            x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        // Configure application
        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}






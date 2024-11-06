using Microsoft.Extensions.Hosting;
using System.Runtime.Versioning;

[SupportedOSPlatform("windows")]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configura para rodar como serviço do Windows, caso seja executado nesse ambiente
        builder.Host.UseWindowsService();

        // Adiciona serviços para controladores
        builder.Services.AddControllers();

        // Configura CORS para permitir requisições de localhost:3000
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost3000",
                policy => policy.WithOrigins("http://localhost:3000")
                                .AllowAnyMethod()
                                .AllowAnyHeader());
        });

        // Configura Swagger para documentação da API (usado apenas em desenvolvimento)
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configuração do pipeline de requisição HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Aplica a política de CORS configurada
        app.UseCors("AllowLocalhost3000");

        // Habilita o roteamento e mapeia os controladores
        app.UseRouting();
        app.MapControllers();

        // Exemplo de endpoint
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", () =>
        {
            var forecast =  Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        // Executa o aplicativo normalmente
        app.Run();
    }
}

// Definição do record para o endpoint de exemplo
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

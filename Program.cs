using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddRazorComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseFileServer();
app.UseStaticFiles();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    Thread.Sleep(5000);
    var sb = new StringBuilder();
    sb.Append("<table>");
    sb.Append("<thead>");
    sb.Append("<tr>");
    sb.Append("<th>Date</th><th>Degrees</th><th>Summary</th>");
    sb.Append("</tr>");
    sb.Append("<tbody>");
    sb.AppendJoin("", Enumerable.Range(1, 5).Select(index =>
        $"<tr><td>{DateOnly.FromDateTime(DateTime.Now.AddDays(index))}</td><td>{Random.Shared.Next(-20, 55)}</td><td>{summaries[Random.Shared.Next(summaries.Length)]}</td></tr>")
        .ToArray());
    sb.Append("</tbody>");
    return sb.ToString();
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

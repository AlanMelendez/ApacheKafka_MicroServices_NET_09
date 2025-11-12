using MediatR;
using Ticketing.Command.Application.Extension;
using Ticketing.Command.Features.Api;
using Ticketing.Command.Features.Extensions;
using Ticketing.Command.Features.Tickets;
using Ticketing.Command.Infrastructure.Extension;
using static Ticketing.Command.Features.Tickets.TicketCreate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Register the minimal Apis with the service collection which we were created:
builder.Services.RegisterAllMinimalApisLikeServices();



//Register Extensions Services here:
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

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
.WithName("GetWeatherForecast");


app.MapGet("/test", () => "Hello World!")
   .WithName("TestMessage");

app.MapPost("/api/ticket", async (IMediator mediator, TicketCreateRequest request, CancellationToken cancellationToken) =>
{

    var command = new TicketCreateCommand(request);
    var response = await mediator.Send(command, cancellationToken);

    return Results.Ok(response);
}).WithName("CreateTicket");


app.MapAllMinimalApiEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

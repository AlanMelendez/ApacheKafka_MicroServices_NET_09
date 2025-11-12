
using Ticketing.Command.Application.Extension;
using Ticketing.Command.Features.Api;
using Ticketing.Command.Features.Extensions;
using Ticketing.Command.Infrastructure.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

//app.UseHttpsRedirection();

app.MapGet("/test", () => "Hello World!")
   .WithName("TestMessage");

// app.MapPost("/api/ticket", async (IMediator mediator, TicketCreateRequest request, CancellationToken cancellationToken) =>
// {

//     var command = new TicketCreateCommand(request);
//     var response = await mediator.Send(command, cancellationToken);

//     return Results.Ok(response);
// }).WithName("CreateTicket");


app.MapAllMinimalApiEndpoints();

app.Run();


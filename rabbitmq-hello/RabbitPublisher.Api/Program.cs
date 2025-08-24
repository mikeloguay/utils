using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();

app.MapGet("/publish", (ILogger<Program> logger) =>
{
    logger.LogInformation("Publishing message to RabbitMQ");
    return Results.Ok("Message published to RabbitMQ");
});

app.Run();
using Microsoft.AspNetCore.Mvc;
using MyLib;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;

builder.Services.AddOpenApi();
builder.Services.AddMyServiceFromLib(config.GetSection("MyOptions"));

var app = builder.Build();

app.MapOpenApi();

app.MapGet("/do", ([FromServices]IMyService service) =>
{
    return service.PrintOptions();
});

app.Run();


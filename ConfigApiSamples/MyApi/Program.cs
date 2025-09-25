using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyLib;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddMyServiceFromLib();

var app = builder.Build();

app.MapOpenApi();

app.MapGet("/do", ([FromServices]IMyService service) =>
{
    return service.PrintOptions();
});

app.Run();


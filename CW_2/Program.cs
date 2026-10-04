using System.Diagnostics;
using System.Text.Json.Serialization;
using CW_2.Models;
using CW_2.Services;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ProductService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.Use(async (context, next) =>
{
    var stopwatch = Stopwatch.StartNew();

    context.Response.OnStarting(() =>
    {
        stopwatch.Stop();
        context.Response.Headers["X-Response-Time-Ms"] = stopwatch.ElapsedMilliseconds.ToString();
        return Task.CompletedTask;
    });

    await next(context);
});

app.UseAuthorization();

app.MapControllers();

app.Run();
//4 Task Postman
//POST - 201, GET - 200, PUT - 204, PATCH - 200, DELETE - 204
//5 Task UI
//Сваггер виглядає більш класично і старомодно. Скаляр вигдядає більш сучасно і приємніше для очей, має більш зручніший інтерфейс
using System.Diagnostics;
using CW_2.Models;
using CW_2.Services;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ProductService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
// Task 7 Пусте тіло
//{
//    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
//  "title": "One or more validation errors occurred.",
//  "status": 400,
//  "errors": {
//        "Name": [
//          "The Name field is required.",
//      "The field Name must be a string with a minimum length of 1 and a maximum length of 100."
//        ],
//    "Price": [
//      "Price must be greater than zero."
//    ],
//    "Category": [
//      "The Category field is required."
//    ]
//  },
//  "traceId": "00-9ddffa7991f44f2eae364f6e1619153b-14d99c36b80c3231-00"
//}
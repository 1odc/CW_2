using System.Diagnostics;
using CW_2.Middleware;
using CW_2.Models;
using CW_2.Services;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ProductService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi("v1", options =>
{
    // Document transformer — місце, де можна дописати метадані, яких немає
    // в самому коді контролерів: назву, опис, версію всього документа.
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "ProductCatalog API";
        document.Info.Version = "v1";
        document.Info.Description =
            "Базова версія API каталогу товарів. Список товарів повертається " +
            "як звичайний масив JSON.";
        return Task.CompletedTask;
    });

    // У документ "v1" включаємо лише дії з GroupName == "v1" (або взагалі без
    // групи — це стосується службових дій фреймворку, яких у нас немає, але
    // так безпечніше на випадок майбутніх змін).
    options.ShouldInclude = apiDescription => apiDescription.GroupName is null or "v1";
});
builder.Services.AddOpenApi("v2", options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "ProductCatalog API";
        document.Info.Version = "v2";
        document.Info.Description =
            "Друга версія API. Головна відмінність від v1: список товарів тепер " +
            "обгорнутий в об'єкт з полями 'data' і 'count', а кожен товар додатково " +
            "містить discountPercent. Так навмисно показано, навіщо потрібне " +
            "версіонування — v1 лишається незмінною для старих клієнтів, поки v2 " +
            "вносить структурну зміну відповіді.";
        return Task.CompletedTask;
    });

    options.ShouldInclude = apiDescription => apiDescription.GroupName is null or "v2";
});
var app = builder.Build();
app.UseMiddleware<DocAccessMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "ProductCatalog v1");
    });
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

//app.MapControllers();

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
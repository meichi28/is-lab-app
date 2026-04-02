using Microsoft.AspNetCore.Mvc;
using NotesApi.Services;  // ← Добавьте эту строку

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Регистрируем сервис проверки БД
builder.Services.AddScoped<DbHealthService>();

var app = builder.Build();

// Swagger только для разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// 🔹 Существующие эндпоинты
app.MapGet("/health", () => new { status = "ok", time = DateTime.Now });

app.MapGet("/version", () => new
{
    name = builder.Configuration["App:Name"],
    version = builder.Configuration["App:Version"]
});

// 🔹 НОВЫЙ эндпоинт: /db/ping
app.MapGet("/db/ping", async (DbHealthService dbService) =>
{
    var (success, message) = await dbService.CheckConnectionAsync();

    if (success)
        return Results.Ok(new { status = message });
    else
        return Results.Problem(message, statusCode: 503);
});

// 🔹 Контроллеры
app.MapControllers();

app.Run();
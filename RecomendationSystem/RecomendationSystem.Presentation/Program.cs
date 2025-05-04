using Microsoft.EntityFrameworkCore;
using RecomandationSystem.Application;
using RecomandationSystem.Application.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddBLL(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

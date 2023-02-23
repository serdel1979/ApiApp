using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("All", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MemoryBufferThreshold = Int32.MaxValue;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("All");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

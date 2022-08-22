using WSColegio.Models;
using WSColegio.Services;
using WSColegio.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add dependencies
builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<IGradoAlumnoService, GradoAlumnoService>();
builder.Services.AddScoped<IGradoService, GradoService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IListService, ListService>();
//Add CORS
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

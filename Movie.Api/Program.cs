using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movie.Api.Client;
using Movie.Api.Data;
using Movie.Api.DTOs;
using Movie.Api.Options;
using Movie.Api.Repositories;
using Movie.Api.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<TmdbOptions>(builder.Configuration.GetSection("Tmdb"));

builder.Services.AddHttpClient<ITmdbClient, TmdbClient>();

//comentado por ahora ya que falta la implementación de los servicios y repositorios***
//builder.Services.AddScoped<IMovieRepository, MovieRepository>();
//builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
//builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
//builder.Services.AddScoped<IGenreRepository, GenreRepository>();

//builder.Services.AddScoped<ITmdbService, TmdbService>();
//builder.Services.AddScoped<ILibraryService, LibraryService>();
//builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MovieTracker API",
        Version = "v1",
        Description = "API para biblioteca personal de películas"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

//comentado por ahora ya que falta la implementación de los servicios y repositorios***
//app.MapGet("/api/dashboard", async (IDashboardService service) =>
//{
//    var dto = await service.GetAsync();
//    return Results.Ok(dto);
//})
//.WithName("GetDashboard")
//.WithTags("Dashboard")
//.Produces<DashboardDto>(StatusCodes.Status200OK);

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
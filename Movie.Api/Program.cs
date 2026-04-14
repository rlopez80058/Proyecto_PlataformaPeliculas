using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Movie.Api.Client;
using Movie.Api.Data;
using Movie.Api.DTOs;
using Movie.Api.Options;
using Movie.Api.Repositories;
using Movie.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<TmdbOptions>(builder.Configuration.GetSection("Tmdb"));
builder.Services.AddHttpClient<ITmdbClient, TmdbClient>();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

builder.Services.AddScoped<ITmdbService, TmdbService>();
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

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

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/api/dashboard", async (IDashboardService service) =>
{
    var dto = await service.GetAsync();
    return Results.Ok(dto);
})
.WithName("GetDashboard")
.WithTags("Dashboard")
.Produces<DashboardDto>(StatusCodes.Status200OK);

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
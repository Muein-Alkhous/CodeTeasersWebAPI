using Application;
using Infrastructure;
using Infrastructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("CodeTeasersConnectionString")));

        builder.Services
            .AddApplication()
            .AddInfrastructure(builder.Configuration);
        
        builder.Services.AddMapster();

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        
        // Add Problem Details
        builder.Services.AddProblemDetails();
        
        //  Add Swagger services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "CodeTeasers",
                Version = "v1"
            });
        });
        
        MappingConfig.Configure();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            // Enable middleware to serve Swagger and Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeTeasers V1");
                c.RoutePrefix = string.Empty; // Serve Swagger UI at root: http://localhost:5000/
            });
        }

        app.UseHttpsRedirection();
        
        app.UseExceptionHandler();
        
        app.UseStatusCodePages();

        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}
using Application.Interfaces;
using Application.Interfaces.Authentication;
using Domain.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        services.Configure<JwtSettings>(builderConfiguration.GetSection(JwtSettings.SectionName));
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProblemRepository, ProblemRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}
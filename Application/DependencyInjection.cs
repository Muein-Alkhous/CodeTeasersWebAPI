using Application.Interfaces;
using Application.Interfaces.Authentication;
using Application.Services;
using Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ICategoryService, CategoryService>();
        return services;
    }
}
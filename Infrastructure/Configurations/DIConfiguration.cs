using Application.Services.Implementations;
using Application.Services.Interfaces;
using Data.UnitOfWork.Implementations;
using Data.UnitOfWork.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class DIConfiguration
{
    public static void AddDependencyInjection(this IServiceCollection service)
    {
        service.AddScoped<IRoleService, RoleService>();
        service.AddScoped<IUserService, UserService>();
        service.AddTransient<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IAuthService, AuthService>();


    }
}
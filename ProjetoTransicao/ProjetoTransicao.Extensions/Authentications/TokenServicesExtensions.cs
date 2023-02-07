using Microsoft.Extensions.DependencyInjection;
using ProjetoTransicao.Extensions.Authentications.Services;

namespace ProjetoTransicao.Extensions.Authentications;

public static class TokenServicesExtensions
{
    public static IServiceCollection AddApiToken(this IServiceCollection services)
    {
        services.AddScoped<ITokenServices, TokenServices>();

        return services;
    }
}

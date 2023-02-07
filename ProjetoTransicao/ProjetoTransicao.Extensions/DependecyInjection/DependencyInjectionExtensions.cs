using Microsoft.Extensions.DependencyInjection;
using ProjetoTransicao.Extensions.Logs.Configurations;
using ProjetoTransicao.Extensions.Notifications;

namespace ProjetoTransicao.Extensions.DependecyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection SolveStructuralAppDependencyInjection(this IServiceCollection services)
    {
        services.AddStructuraLog();
        services.AddNotificationControl();

        return services;
    }
}

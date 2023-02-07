using Microsoft.Extensions.DependencyInjection;
using ProjetoTransicao.Shared.Notifications;

namespace ProjetoTransicao.Extensions.Notifications;

public static class NotificationExtensions
{
    public static IServiceCollection AddNotificationControl(this IServiceCollection services)
    {
        services.AddSingleton<INotificationServices, NotificationServices>();
        return services;
    }
}

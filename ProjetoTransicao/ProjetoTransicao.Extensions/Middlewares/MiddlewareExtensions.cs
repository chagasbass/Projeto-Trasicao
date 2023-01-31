using Microsoft.Extensions.DependencyInjection;

namespace ProjetoTransicao.Extensions.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddGlobalCustomsMiddlewares(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandlerMiddleware>();
            return services;
        }
    }
}

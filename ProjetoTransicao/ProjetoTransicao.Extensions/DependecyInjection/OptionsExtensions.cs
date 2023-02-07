using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoTransicao.Shared.Configurations;

namespace ProjetoTransicao.Extensions.DependecyInjection;

public static class OptionsExtensions
{
    public static IServiceCollection AddOptionsPattern(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BaseConfigurationOptions>(configuration.GetSection(BaseConfigurationOptions.BaseConfig));
        services.Configure<HealthchecksConfigurationOptions>(configuration.GetSection(HealthchecksConfigurationOptions.BaseConfig));
        services.Configure<ProblemDetailConfigurationOptions>(configuration.GetSection(ProblemDetailConfigurationOptions.BaseConfig));
        services.Configure<ResilienceConfigurationOptions>(configuration.GetSection(ResilienceConfigurationOptions.BaseConfig));
        services.Configure<CacheConfigurationOptions>(configuration.GetSection(CacheConfigurationOptions.BaseConfig));

        return services;
    }
}

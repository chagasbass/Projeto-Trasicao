using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ProjetoTransicao.Extensions.Documentations
{
    public static class VersioningExtensions
    {
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            /*
             * ReportApiVersions = add a versão da api no Response header
             *AssumeDefaultVersionWhenUnspecified = caso não seja especificada a versão , pega a versão default
             *DefaultApiVersion = versão default da api
             *ApiVersionReader = add o versão no header
             */

            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                //opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
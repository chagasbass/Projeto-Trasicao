using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ProjetoTransicao.Api.Bases;

namespace ProjetoTransicao.API.Extensions
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder webApplicationBuilder) where TStartup : IBaseStartup
        {
            var startup = Activator.CreateInstance(typeof(TStartup), webApplicationBuilder.Configuration) as IBaseStartup;

            if (startup is null)
                throw new ArgumentException("Classe Startup.cs inválida");

            startup.ConfigureServices(webApplicationBuilder.Services);

            var app = webApplicationBuilder.Build();

            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            startup.Configure(app, app.Environment, provider);

            app.Run();

            return webApplicationBuilder;

        }
    }
}

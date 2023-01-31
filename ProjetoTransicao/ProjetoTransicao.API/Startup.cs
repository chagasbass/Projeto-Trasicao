using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ProjetoTransicao.Api.Bases;
using ProjetoTransicao.Extensions.Middlewares;

namespace ProjetoTransicao.API
{
    public class Startup : IBaseStartup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(WebApplication app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer()
                    .AddGlobalCustomsMiddlewares();
        }
    }

}

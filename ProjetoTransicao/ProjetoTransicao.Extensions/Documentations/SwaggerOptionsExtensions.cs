using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjetoTransicao.Extensions.Documentations
{
    public class SwaggerOptionsExtensions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IConfiguration _configuration;

        const string mensagemPadrao = "Não informado";
        const string nomePadrao = "Minha - API";
        const string urlPadrao = @"https://www.google.com/";

        public SwaggerOptionsExtensions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
        {
            _provider = provider;
            _configuration = configuration;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(
            ApiVersionDescription description)
        {
            var applicationName = _configuration["BaseConfiguration:NomeAplicacao"];
            var applicationDescription = _configuration["BaseConfiguration:Descricao"];
            var developerName = _configuration["BaseConfiguration:Desenvolvedor"];
            var companyName = _configuration["BaseConfiguration:NomeEmpresa"];
            var companyUrl = _configuration["BaseConfiguration:UrlEmpresa"];

            if (string.IsNullOrEmpty(applicationName))
                applicationName = nomePadrao;

            if (string.IsNullOrEmpty(companyUrl))
                companyUrl = urlPadrao;

            if (string.IsNullOrEmpty(companyName))
                companyName = mensagemPadrao;

            if (string.IsNullOrEmpty(developerName))
                developerName = mensagemPadrao;

            var uri = new Uri(companyUrl);

            var info = new OpenApiInfo
            {
                Title = applicationName,
                Description = $"{applicationDescription} Desenvolvido por: {developerName}",
                License = new OpenApiLicense { Name = companyName, Url = uri },
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " Esta versão de API está depreciada.";
            }

            return info;
        }
    }
}
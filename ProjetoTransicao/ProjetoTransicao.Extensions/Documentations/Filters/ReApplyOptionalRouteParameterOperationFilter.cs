using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjetoTransicao.Extensions.Documentations.Filters
{
    /// <summary>
    /// Classe para o swagger entender quando existe o parâmetro de rota que não é obrigatório.
    /// </summary>
    public class ReApplyOptionalRouteParameterOperationFilter : IOperationFilter
    {
        const string captureName = "routeParameter";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var httpMethodAttributes = context.MethodInfo
                 .GetCustomAttributes(true)
                 .OfType<Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute>();

            var httpMethodWithOptional = httpMethodAttributes?.FirstOrDefault(m => m.Template?.Contains("?") ?? false);
            if (httpMethodWithOptional == null)
                return;

            string regex = $"{{(?<{captureName}>\\w+)\\?}}";

            var matches = System.Text.RegularExpressions.Regex.Matches(httpMethodWithOptional.Template, regex);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                var name = match.Groups[captureName].Value;

                var parameter = operation.Parameters.FirstOrDefault(p => p.In == ParameterLocation.Path && p.Name == name);

                if (parameter != null)
                {
                    parameter.AllowEmptyValue = true;
                    parameter.Description = "A opção \"Send empty value\" deve ser marcada caso não seja efetuada a passagem do parâmetro";
                    parameter.Required = false;
                    //parameter.Schema.Default = new OpenApiString(string.Empty);
                    parameter.Schema.Nullable = true;
                }
            }
        }
    }
}
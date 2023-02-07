using ProjetoTransicao.API;
using ProjetoTransicao.API.Extensions;
using ProjetoTransicao.Extensions.Logs.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

#region configuring logs
Log.Logger = LogExtensions.ConfigureStructuralLogWithSerilog(configuration);
builder.Logging.AddSerilog(Log.Logger);
#endregion
try
{
    Log.Information("Iniciando a aplicação");
    builder.UseStartup<Startup>();
}
catch (Exception ex)
{
    Log.Fatal($"Erro fatal na aplicação => {ex.Message}");
}
finally
{
    Log.CloseAndFlush();
}
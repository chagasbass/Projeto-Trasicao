using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjetoTransicao.Extensions.Logs.Services;
using ProjetoTransicao.Factories;
using ProjetoTransicao.Shared.Configurations;
using ProjetoTransicao.Shared.Entities;
using ProjetoTransicao.Shared.Enums;
using System.Text.Json;

namespace ProjetoTransicao.Extensions.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly ProblemDetailConfigurationOptions _problemOptions;
    private readonly ILogServices _logServices;

    public GlobalExceptionHandlerMiddleware(IOptions<ProblemDetailConfigurationOptions> options,
                                            ILogServices logServices)
    {
        _problemOptions = options.Value;
        _logServices = logServices;
    }

    private ProblemDetails ConfigureProblemDetails(int statusCode, Exception exception, HttpContext context, string defaultDetail = "")
    {
        var defaultTitle = "Um erro ocorreu ao processar o request.";

        if (string.IsNullOrEmpty(defaultDetail))
        {
            defaultDetail = $"Erro fatal na aplicação,entre em contato com um Desenvolvedor responsável. Causa: {exception.Message}";
        }

        var title = _problemOptions.Title;
        var detail = _problemOptions.Detail;
        var instance = context.Request.HttpContext.Request.Path.ToString();

        var type = StatusCodeOperation.RetrieveStatusCode(statusCode);

        if (string.IsNullOrEmpty(title))
            title = defaultTitle;

        if (string.IsNullOrEmpty(detail))
            detail = defaultDetail;

        return new ProblemDetails()
        {
            Detail = detail,
            Instance = instance,
            Status = statusCode,
            Title = title,
            Type = type.Text
        };
    }


    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        const string dataType = @"application/problem+json";
        const int statusCode = StatusCodes.Status500InternalServerError;

        var commandResult = new CommandResult();

        if (exception is JsonException)
        {
            var statusCodeJsonException = StatusCodes.Status400BadRequest;

            var problemDetailsJson = ConfigureProblemDetails(statusCodeJsonException, exception, context, "O serviço de endereços está fora do ar tente novamente mais tarde");

            commandResult.Data = problemDetailsJson;

            context.Response.StatusCode = statusCodeJsonException;
            context.Response.ContentType = dataType;

            _logServices.LogData.AddResponseStatusCode(statusCodeJsonException)
                                .AddResponseBody(commandResult);

            _logServices.WriteLog();

            await context.Response.WriteAsync(JsonSerializer.Serialize(commandResult, JsonOptionsFactory.GetSerializerOptions()));
        }
        else
        {
            _logServices.LogData.AddException(exception)
                                .AddResponseStatusCode(statusCode)
                                .AddResponseBody(commandResult);

            _logServices.WriteLog();
            _logServices.WriteLogWhenRaiseExceptions();

            var problemDetails = ConfigureProblemDetails(statusCode, exception, context);

            var commandResultDefault = new CommandResult(problemDetails);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = dataType;

            await context.Response.WriteAsync(JsonSerializer.Serialize(commandResultDefault, JsonOptionsFactory.GetSerializerOptions()));
        }

    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
}

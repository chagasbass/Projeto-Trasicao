namespace ProjetoTransicao.Shared.Entities;

/// <summary>
/// Objeto que representa um retorno customizado para api
/// </summary>
public class CommandResult : ICommandResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public object Data { get; set; }

    public CommandResult() { }

    public CommandResult(object data, bool success = false, string message = "")
    {
        Success = success;
        Message = message;
        Data = data;
    }
}
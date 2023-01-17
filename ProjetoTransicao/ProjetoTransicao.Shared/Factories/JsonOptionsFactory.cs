namespace ProjetoTransicao.Factories;

public static class JsonOptionsFactory
{
    public static JsonSerializerOptions GetSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
    }
}
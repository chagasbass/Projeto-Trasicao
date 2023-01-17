namespace ProjetoTransicao.Shared.Helpers;

public static class DateTimeExtensions
{
    public static DateTime GetGmtDateTime(this DateTime data) => data.AddHours(-3);
}
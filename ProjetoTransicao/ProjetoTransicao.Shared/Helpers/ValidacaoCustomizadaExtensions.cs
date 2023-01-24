using System.Text.RegularExpressions;

namespace ProjetoTransicao.Shared.Helpers;

public static class ValidacaoCustomizadaExtensions
{
    private static readonly List<string> _CepsInvalidos = new()
        {
            "00000000",
            "11111111",
            "22222222",
            "33333333",
            "44444444",
            "55555555",
            "66666666",
            "77777777",
            "99999999",
            "00000-000",
            "11111-111",
            "22222-222",
            "33333-333",
            "44444-444",
            "55555-555",
            "66666-666",
            "77777-777",
            "99999-999",
        };

    public static bool ValidarCep(this string cep)
    {
        if (string.IsNullOrEmpty(cep))
            return false;

        if (_CepsInvalidos.Contains(cep))
            return false;

        if (cep.Contains("-"))
        {
            if (cep.Length != 9)
                return false;

            return Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
        }
        else
        {
            if (cep.Length != 8)
                return false;
        }

        cep = cep.Insert(5, "-");

        return Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
    }
}
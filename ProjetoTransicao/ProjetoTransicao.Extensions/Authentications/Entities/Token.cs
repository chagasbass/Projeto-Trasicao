namespace ProjetoTransicao.Extensions.Authentications.Entities;

public class Token
{
    public string? Email { get; set; }
    public string? TokenGerado { get; set; }
    public DateTime DataExpiracao { get; set; }

    public Token() { }
}
using ProjetoTransicao.Extensions.Authentications.Entities;

namespace ProjetoTransicao.Extensions.Authentications.Services;

public interface ITokenServices
{
    Token GerarToken(string email);
}

using Microsoft.IdentityModel.Tokens;
using ProjetoTransicao.Extensions.Authentications.Entities;
using ProjetoTransicao.Shared.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoTransicao.Extensions.Authentications.Services;

public class TokenServices : ITokenServices
{
    public Token GerarToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(SharedExtensions.SEGREDO_TOKEN);

        var expirationDate = DateTime.UtcNow.AddMinutes(SharedExtensions.TEMPO_EXPIRACAO_EM_MINUTOS);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Role, SharedExtensions.USER_DEFAULT_ROLE)
            }),
            Expires = expirationDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new Token
        {
            TokenGerado = tokenHandler.WriteToken(token),
            DataExpiracao = expirationDate,
            Email = email
        };
    }
}

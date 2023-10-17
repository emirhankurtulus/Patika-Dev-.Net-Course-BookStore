using Microsoft.IdentityModel.Tokens;
using Project.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Services;

public class AuthenticationService : IAuthenticationService
{
    public IConfiguration _configuration { get; set; }

    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenDto CreateAccesToken(Guid userId)
    {
        var claims = new List<Claim>
    {
        new Claim("userId", userId.ToString())
    };

        var token = new TokenDto();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        token.Expiration = DateTime.Now.AddMinutes(15);

        var securityToken = new JwtSecurityToken(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            claims: claims,
            signingCredentials: credentials
            );

        var tokenHandler = new JwtSecurityTokenHandler();

        token.AccessToken = tokenHandler.WriteToken(securityToken);

        token.RefreshToken = CreateRefreshToken();

        return token;
    }

    public bool ValidateToken(Guid userId, string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken ?? throw new ArgumentException("Invalid JWT token");

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId");

        if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
        {
            throw new ArgumentException("UserId not found in the token");
        }

        if(userId.ToString() == userIdClaim.Value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Guid GetIdByToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken ?? throw new ArgumentException("Invalid JWT token");

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId");

        return Guid.Parse(userIdClaim.Value);
    }


    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
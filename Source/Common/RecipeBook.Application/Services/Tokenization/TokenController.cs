using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecipeBook.Application.Services.Tokenization;

public class TokenController
{
    private const string EmailAlias = "eml";
    private const string NameAlias = "nam";
    private const string PhoneAlias = "phn";

    private readonly string _securityKey;
    private readonly double _tokenExpiration;
    public TokenController(string securityKey, double tokenExpiration)
    {
        _securityKey = securityKey;
        _tokenExpiration = tokenExpiration;
    }

    public string GenerateToken(string email, string name, string phone)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_securityKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(EmailAlias, email),
                new Claim(NameAlias, name),
                new Claim(PhoneAlias, phone)
            }),
            Expires = DateTime.UtcNow.AddHours(_tokenExpiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public void ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_securityKey);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,

        }, out _);
    }

    public string GetEmailFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_securityKey);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        return jwtToken.Claims.First(x => x.Type == EmailAlias).Value;
    }

    public string GetNameFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_securityKey);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        return jwtToken.Claims.First(x => x.Type == NameAlias).Value;
    }

    public string GetPhoneFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_securityKey);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        return jwtToken.Claims.First(x => x.Type == PhoneAlias).Value;
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RecipeBook.Application.Services.Tokenization;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(IdentityUser user)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id),
                    new Claim("ml", user.Email!),
                    new Claim("phn", user.PhoneNumber!)
                }),
                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["Jwt:ExpiryTimeFrame"]!)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public string GenerateRefreshToken()
    {
        try
        {
            var randomBytes = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string GetEmailFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                throw new SecurityTokenExpiredException(ErrorMessages.TOKEN_EXPIRADO);
            }

            return jwtToken.Claims.First(x => x.Type == "ml").Value;
        }
        catch (SecurityTokenExpiredException)
        {
            throw new ExceptionToken(new List<string> { ErrorMessages.TOKEN_EXPIRADO });

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string GetPhoneFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                throw new SecurityTokenExpiredException(ErrorMessages.TOKEN_EXPIRADO);
            }

            return jwtToken.Claims.First(x => x.Type == "pnm").Value;
        }
        catch (SecurityTokenExpiredException)
        {
            throw new ExceptionToken(new List<string> { ErrorMessages.TOKEN_EXPIRADO });

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Guid GetIdFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                throw new SecurityTokenExpiredException(ErrorMessages.TOKEN_EXPIRADO);
            }

            return new Guid(jwtToken.Claims.First(x => x.Type == "id").Value);
        }
        catch (SecurityTokenExpiredException)
        {
            throw new ExceptionToken(new List<string> { ErrorMessages.TOKEN_EXPIRADO });

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
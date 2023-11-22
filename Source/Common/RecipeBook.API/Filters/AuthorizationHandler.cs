using Microsoft.AspNetCore.Authorization;
using RecipeBook.Application.Services.Tokenization;

namespace RecipeBook.API.Filters;

public class AuthorizationHandler : AuthorizationHandler<AuthorizationRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenService _token;

    public AuthorizationHandler(IHttpContextAccessor httpContextAccessor, ITokenService token)
    {
        _httpContextAccessor = httpContextAccessor;
        _token = token;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AuthorizationRequirement requirement)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()
            .Replace("Bearer ", "");

        if (string.IsNullOrWhiteSpace(token)) context.Fail();

        var valid = _token.ValidateToken(token!);

        if (!valid) context.Fail();

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
using Microsoft.AspNetCore.Authorization;

namespace RecipeBook.API.WebSockets;

public class AuthorizationHub : AuthorizationHandler<AuthorizationRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationHub(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AuthorizationRequirement requirement)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString()
            .Replace("Bearer ", string.Empty);

        if (string.IsNullOrWhiteSpace(token)) context.Fail();

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

public class AuthorizationRequirement : IAuthorizationRequirement
{
}
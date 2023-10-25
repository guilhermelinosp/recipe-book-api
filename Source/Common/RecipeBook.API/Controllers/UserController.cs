using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.Users.SignUp;
using RecipeBook.Domain.Dtos.Requests;

namespace RecipeBook.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ISignUpUseCase _signUpUseCase;
    private readonly IAntiforgery _antiforgery;


    public UserController(ISignUpUseCase signUpUseCase, IAntiforgery antiforgery)
    {
        _signUpUseCase = signUpUseCase;
        _antiforgery = antiforgery;
    }

    [HttpPost("antiforgery")]
    public IActionResult AntiForgery()
    {
        // Gere o token anti-forgery
        var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
        return Ok(new { csrf = tokens.RequestToken });
    }

    [HttpPost("sign-up")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request, [FromHeader(Name = "x-csrf")] string csrfToken)
    {
        await _signUpUseCase.ExecuteAsync(request);
        return Ok();
    }
}
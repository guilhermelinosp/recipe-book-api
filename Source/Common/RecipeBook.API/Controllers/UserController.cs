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

    [HttpGet("antiforgery")]
    [IgnoreAntiforgeryToken]
    public IActionResult AntiForgery()
    {
        var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
        return Ok(new { csrf = tokens.RequestToken });
    }

    [HttpPost("sign-up")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> SignUp([FromBody] RequestSignUp request, [FromHeader(Name = "csrf")] string? csrf)
    {
        await _signUpUseCase.ExecuteAsync(request);
        return Ok();
    }
}
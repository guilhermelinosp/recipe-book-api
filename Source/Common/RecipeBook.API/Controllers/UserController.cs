using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.Users.SignIn;
using RecipeBook.Application.UseCases.Users.SignUp;
using RecipeBook.Domain.Dtos.Requests;

namespace RecipeBook.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ISignUpUseCase _signUpUseCase;
    private readonly ISignInUseCase _signInUseCase;


    public UserController(ISignUpUseCase signUpUseCase, ISignInUseCase signInUseCase)
    {
        _signUpUseCase = signUpUseCase;
        _signInUseCase = signInUseCase;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        await _signUpUseCase.ExecuteAsync(request);
        return Created(string.Empty, null);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var tokens = await _signInUseCase.ExecuteAsync(request);
        return Ok(tokens);
    }
}
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.Users.SignUp;
using RecipeBook.Domain.Dtos.Requests;

namespace RecipeBook.API.Controllers;

[ApiController]
[ValidateAntiForgeryToken]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ISignUpUseCase _signUpUseCase;

    public UserController(ISignUpUseCase signUpUseCase)
    {
        _signUpUseCase = signUpUseCase;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        await _signUpUseCase.ExecuteAsync(request);
        return Ok();
    }
}
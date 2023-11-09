using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.Accounts.EmailConfirmation;
using RecipeBook.Application.UseCases.Accounts.ForgotPassword;
using RecipeBook.Application.UseCases.Accounts.ResetPassword;
using RecipeBook.Application.UseCases.Accounts.SignIn;
using RecipeBook.Application.UseCases.Accounts.SignUp;
using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Domain.Dtos.Responses.Account;

namespace RecipeBook.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ISignUpUseCase _signUp;
    private readonly ISignInUseCase _signIn;
    private readonly IForgotPasswordUseCase _forgotPassword;
    private readonly IResetPasswordUseCase _resetPassword;
    private readonly IEmailConfirmation _emailConfirmation;

    public AccountController(ISignUpUseCase signUp, ISignInUseCase signIn, IForgotPasswordUseCase forgotPassword, IResetPasswordUseCase resetPassword, IEmailConfirmation emailConfirmation)
    {
        _signUp = signUp;
        _signIn = signIn;
        _forgotPassword = forgotPassword;
        _resetPassword = resetPassword;
        _emailConfirmation = emailConfirmation;
    }

    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        await _signUp.SignUpAsync(request);
        return Ok(new { message = "Confirm your email to use your account." });
    }

    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("email-confirmation")]
    public async Task<IActionResult> EmailConfirmation([FromBody] EmailConfirmationRequest request)
    {
        await _emailConfirmation.EmailConfirmationAsync(request);
        return Ok(new { message = "Email confirmed successfully." });
    }

    [AllowAnonymous]
    [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var response = await _signIn.SignInAsync(request);
        return Ok(response);
    }

    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await _forgotPassword.ForgoPasswordAsync(request);
        return Ok(new { message = "Security code sent to your email." });
    }

    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        await _resetPassword.ResetPasswordAsync(request);
        return Ok(new { message = "Password reset successful." });
    }

    //[AllowAnonymous]
    //[HttpPost("change-password")]
    //public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    //{
    //    await _changePassword.ChangePasswordAsync(request);
    //    return Ok();
    //}

    //[AllowAnonymous]
    //[HttpPost("change-email")]
    //public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
    //{
    //    await _changeEmail.ChangeEmailAsync(request);
    //    return Ok();
    //}

    //[AllowAnonymous]
    //[HttpPost("change-phone")]
    //public async Task<IActionResult> ChangePhone([FromBody] ChangePhoneRequest request)
    //{
    //    await _changePhone.ChangePhoneAsync(request);
    //    return Ok();
    //}

    //[AllowAnonymous]
    //[HttpPost("change-name")]
    //public async Task<IActionResult> ChangeName([FromBody] ChangeNameRequest request)
    //{
    //    await _changeName.ChangeNameAsync(request);
    //    return Ok();
    //}

    //[AllowAnonymous]
    //[HttpPost("change-profile-picture")]
    //public async Task<IActionResult> ChangeProfilePicture([FromBody] ChangeProfilePictureRequest request)
    //{
    //    await _changeProfilePicture.ChangeProfilePictureAsync(request);
    //    return Ok();
    //}

    //[AllowAnonymous]
    //[HttpPost("change-address")]
    //public async Task<IActionResult> ChangeAddress([FromBody] ChangeAddressRequest request)
    //{
    //    await _changeAddress.ChangeAddressAsync(request);
    //    return Ok();
    //}

    //[AllowAnonymous]
    //[HttpPost("change-birthdate")]
    //public async Task<IActionResult> ChangeBirthdate([FromBody] ChangeBirthdateRequest request)
    //{
    //    await _changeBirthdate.ChangeBirthdateAsync(request);
    //    return Ok();
    //}
}
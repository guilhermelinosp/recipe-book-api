using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.Accounts;

namespace RecipeBook.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly IAccountUseCase _account;

        public TestController(IAccountUseCase account)
        {
            _account = account;
        }

        [HttpPost]
        public async Task<IActionResult> TestAuth()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = await _account.GetMyAccountAsync(token);
            return Ok(user);
        }
    }
}
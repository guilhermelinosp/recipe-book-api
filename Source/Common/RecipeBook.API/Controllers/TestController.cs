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
        private readonly TestUseCase _test;

        public TestController(TestUseCase test)
        {
            _test = test;
        }

        [HttpGet("MyAccount")]
        public async Task<IActionResult> TestAuth()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = await _test.GetMyAccountAsync(token);
            return Ok(user);
        }
    }
}
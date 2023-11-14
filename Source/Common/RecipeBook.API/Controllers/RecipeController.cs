using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.Recipes.CreateRecipe;
using RecipeBook.Application.UseCases.Recipes.FindRecipe;
using RecipeBook.Domain.Dtos.Requests.Recipes;
using RecipeBook.Domain.Dtos.Responses.Recipes;

namespace RecipeBook.API.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class RecipeController : ControllerBase
{
    private readonly ICreateRecipeUseCase _createRecipe;
    private readonly IFindRecipesByAccountUseCase _findRecipesByAccount;

    public RecipeController(ICreateRecipeUseCase createRecipe, IFindRecipesByAccountUseCase findRecipesByAccount)
    {
        _createRecipe = createRecipe;
        _findRecipesByAccount = findRecipesByAccount;
    }

    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("create-recipe")]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeRequest body)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        await _createRecipe.CreateRecipeAsync(token, body);
        return Created(string.Empty, null);
    }

    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RecipeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("find-recipes")]
    public async Task<IActionResult> FindRecipes([FromQuery] FindRecipeRequest? query)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        var response = await _findRecipesByAccount.FindRecipesAsync(token, query)!;

        return response!.Any() ? Ok(response) : NoContent();
    }

}
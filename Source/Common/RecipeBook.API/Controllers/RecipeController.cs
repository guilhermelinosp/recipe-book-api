using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.Recipes.CreateRecipe;
using RecipeBook.Application.UseCases.Recipes.FindRecipe;
using RecipeBook.Application.UseCases.Recipes.FindRecipeByRecipeId;
using RecipeBook.Domain.Dtos.Requests.Recipes;
using RecipeBook.Domain.Dtos.Responses.Exceptions;
using RecipeBook.Domain.Dtos.Responses.Recipes;

namespace RecipeBook.API.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class RecipeController : ControllerBase
{
    private readonly ICreateRecipeUseCase _createRecipe;
    private readonly IFindRecipesUseCase _findRecipes;
    private readonly IFindRecipeByRecipeIdUseCase _findRecipeByRecipeId;

    public RecipeController(ICreateRecipeUseCase createRecipe, IFindRecipesUseCase findRecipes, IFindRecipeByRecipeIdUseCase findRecipeByRecipeId)
    {
        _createRecipe = createRecipe;
        _findRecipes = findRecipes;
        _findRecipeByRecipeId = findRecipeByRecipeId;
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpPost("create-recipe")]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeRequest body)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        await _createRecipe.CreateRecipeAsync(token, body);
        return Created(string.Empty, null);
    }

    [ProducesResponseType(typeof(IEnumerable<RecipeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("find-recipes")]
    public async Task<IActionResult> FindRecipes([FromQuery] FindRecipeRequest? query)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        var response = await _findRecipes.FindRecipesAsync(token, query);

        return response!.Any() ? Ok(response) : NoContent();
    }

    [ProducesResponseType(typeof(RecipeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("find-recipe-by-recipe-id")]
    public async Task<IActionResult> FindRecipeByRecipeId([FromQuery] Guid recipeId)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        var response = await _findRecipeByRecipeId.FindRecipeByRecipeIdAsync(token, recipeId);

        return Ok(response);
    }

}
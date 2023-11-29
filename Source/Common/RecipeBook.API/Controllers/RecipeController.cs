using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.Recipes.CreateRecipe;
using RecipeBook.Application.UseCases.Recipes.DeleteRecipe;
using RecipeBook.Application.UseCases.Recipes.FindRecipe;
using RecipeBook.Application.UseCases.Recipes.FindRecipeById;
using RecipeBook.Application.UseCases.Recipes.UpdateRecipe;
using RecipeBook.Domain.Dtos.Requests.Recipes;
using RecipeBook.Domain.Dtos.Responses.Exceptions;
using RecipeBook.Domain.Dtos.Responses.Recipes;

namespace RecipeBook.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
[Produces("application/json")]
public class RecipeController : ControllerBase
{
    private readonly ICreateRecipeUseCase _createRecipe;
    private readonly IDeleteRecipeUseCase _deleteRecipe;
    private readonly IFindRecipeByIdUseCase _findRecipeById;
    private readonly IFindRecipesUseCase _findRecipes;
    private readonly IUpdateRecipeUseCase _updateRecipe;

    public RecipeController(ICreateRecipeUseCase createRecipe, IFindRecipesUseCase findRecipes,
        IFindRecipeByIdUseCase findRecipeById, IDeleteRecipeUseCase deleteRecipe, IUpdateRecipeUseCase updateRecipe)
    {
        _createRecipe = createRecipe;
        _findRecipes = findRecipes;
        _findRecipeById = findRecipeById;
        _deleteRecipe = deleteRecipe;
        _updateRecipe = updateRecipe;
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("create-recipe")]
    public async Task<IActionResult> CreateRecipeAsync([FromBody] CreateRecipeRequest body)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        await _createRecipe.CreateRecipeAsync(token, body);
        return Created(string.Empty, null);
    }

    [ProducesResponseType(typeof(IEnumerable<RecipeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [HttpGet("find-recipes")]
    public async Task<IActionResult> FindRecipes([FromQuery] FindRecipeRequest? query)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        return Ok(await _findRecipes.FindRecipesAsync(token, query));
    }

    [ProducesResponseType(typeof(RecipeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [HttpGet("find-recipes/{recipeId}")]
    public async Task<IActionResult> FindRecipeByIdAsync(Guid recipeId)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        return Ok(await _findRecipeById.FindRecipeByRecipeIdAsync(token, recipeId));
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("delete-recipes/{recipeId}")]
    public async Task<IActionResult> DeleteRecipeByIdAsync(Guid recipeId)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        await _deleteRecipe.DeleteRecipeByIdAsync(token, recipeId);
        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [HttpPut("updade-recipes/{recipeId}")]
    public async Task<IActionResult> UpdadeRecipeByIdAsync(Guid recipeId, [FromBody] UpdateRecipeRequest body)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        await _updateRecipe.UpdateRecipeAsync(token, recipeId, body);
        return NoContent();
    }
}
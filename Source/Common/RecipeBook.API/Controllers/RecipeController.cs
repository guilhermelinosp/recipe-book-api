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
	[Produces("application/json")]
	[HttpPost("create-recipe")]
	public async Task<IActionResult> CreateRecipeAsync([FromBody] CreateRecipeRequest body)
	{
		await _createRecipe.CreateRecipeAsync(
			Request.Headers["Authorization"].ToString().Replace("Bearer ", ""), body);
		return Created(string.Empty, null);
	}

	[ProducesResponseType(typeof(IEnumerable<RecipeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
	[Produces("application/json")]
	[HttpGet("find-recipes")]
	public async Task<IActionResult> FindRecipes([FromQuery] FindRecipeRequest? query)
	{
		return Ok(await _findRecipes.FindRecipesAsync(
			Request.Headers["Authorization"].ToString().Replace("Bearer ", ""), query));
	}

	[ProducesResponseType(typeof(RecipeResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
	[Produces("application/json")]
	[HttpGet("find-recipes/{recipeId}")]
	public async Task<IActionResult> FindRecipeByIdAsync(Guid recipeId)
	{
		return Ok(await _findRecipeById.FindRecipeByRecipeIdAsync(
			Request.Headers["Authorization"].ToString().Replace("Bearer ", ""), recipeId));
	}

	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
	[Produces("application/json")]
	[HttpDelete("delete-recipes/{recipeId}")]
	public async Task<IActionResult> DeleteRecipeByIdAsync(Guid recipeId)
	{
		await _deleteRecipe.DeleteRecipeByIdAsync(
			Request.Headers["Authorization"].ToString().Replace("Bearer ", ""), recipeId);
		return NoContent();
	}
	
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
	[Produces("application/json")]
	[HttpPut("updade-recipes/{recipeId}")]
	public async Task<IActionResult> UpdadeRecipeByIdAsync(Guid recipeId, [FromBody] UpdateRecipeRequest body)
	{
		await _updateRecipe.UpdateRecipeAsync(
			Request.Headers["Authorization"].ToString().Replace("Bearer ", ""), recipeId, body);
		return NoContent();
	}
}
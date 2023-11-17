using AutoMapper;
using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Dtos.Requests.Recipes;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Recipes.CreateRecipe;

public class CreateRecipeUseCase : ICreateRecipeUseCase
{
    private readonly IMapper _mapper;
    private readonly IRecipeRepository _repository;
    private readonly ITokenService _token;

    public CreateRecipeUseCase(ITokenService token, IRecipeRepository repository, IMapper mapper)
    {
        _token = token;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task CreateRecipeAsync(string token, CreateRecipeRequest request)
    {
        var accountId = _token.GetIdFromToken(token);

        var validationResult = await new CreateRecipeValidator().ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidatorException(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var recipeWithSameTitle = await _repository.FindRecipeByTitleAsync(request.Title, accountId);
        if (recipeWithSameTitle != null)
            throw new RecipeException(new List<string> { ErrorMessages.RECEITA_TITULO_JA_CADASTRADO });

        var recipe = _mapper.Map<Recipe>(request);

        recipe.AccountId = accountId;

        await _repository.CreateRecipeAsync(recipe);
    }
}
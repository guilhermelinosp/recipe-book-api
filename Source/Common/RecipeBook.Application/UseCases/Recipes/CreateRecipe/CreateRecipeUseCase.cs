using AutoMapper;
using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Dtos.Requests.Recipes;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Recipes.CreateRecipe;

public class CreateRecipeUseCase : ICreateRecipeUseCase
{
    private readonly ITokenService _token;
    private readonly IRecipeRepository _repository;
    private readonly IMapper _mapper;

    public CreateRecipeUseCase(ITokenService token, IRecipeRepository repository, IMapper mapper)
    {
        _token = token;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task CreateRecipeAsync(string token, CreateRecipeRequest request)
    {
        var validator = new CreateRecipeValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid) throw new ExceptionValidator(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        Console.WriteLine(request);

        var accountId = _token.GetIdFromToken(token);

        var recipe = _mapper.Map<Recipe>(request);

        recipe.AccountId = accountId;

        await _repository.CreateRecipeAsync(recipe);
    }
}
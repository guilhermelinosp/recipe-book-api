using AutoMapper;
using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Dtos.Requests.Recipes;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Recipes.UpdateRecipe;

public class UpdateRecipeUseCase : IUpdateRecipeUseCase
{
    private readonly IMapper _mapper;
    private readonly IRecipeRepository _repository;
    private readonly ITokenService _token;


    public UpdateRecipeUseCase(IMapper mapper, IRecipeRepository repository, ITokenService token)
    {
        _mapper = mapper;
        _repository = repository;
        _token = token;
    }

    public async Task UpdateRecipeAsync(string token, Guid receitaId, UpdateRecipeRequest request)
    {
        var accountId = _token.ValidateToken(token);

        var validationResult = await new UpdateRecipeValidator().ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidatorException(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var recipe = await _repository.FindRecipeByIdAsync(receitaId, accountId);
        if (recipe is null)
            throw new RecipeException(new List<string> { ErrorMessages.RECEITA_NAO_ENCONTRADO });

        var recipeWithSameTitle = await _repository.FindRecipeByTitleAsync(request.Title, accountId);
        if (recipeWithSameTitle != null)
            throw new RecipeException(new List<string> { ErrorMessages.RECEITA_TITULO_JA_CADASTRADO });

        _mapper.Map(request, recipe);

        await _repository.UpdateRecipeAsync(recipe);
    }
}
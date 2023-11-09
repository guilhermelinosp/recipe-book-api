using FluentValidation;
using RecipeBook.Domain.Dtos.Requests.Recipes;
using RecipeBook.Exceptions;

namespace RecipeBook.Application.UseCases.Recipes.CreateRecipe;

public class CreateRecipeValidator : AbstractValidator<CreateRecipeRequest>
{
    public CreateRecipeValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Category).IsInEnum().WithMessage(ErrorMessages.CATEGORIA_RECEITA_INVALIDA);
        RuleFor(x => x.PreparationMode).NotEmpty();
        RuleFor(x => x.Ingredients).NotEmpty();
        RuleForEach(x => x.Ingredients)
            .ChildRules(i =>
            {
                i.RuleFor(x => x.Product).NotEmpty();
                i.RuleFor(x => x.Quantity).NotEmpty();
            });

    }
}
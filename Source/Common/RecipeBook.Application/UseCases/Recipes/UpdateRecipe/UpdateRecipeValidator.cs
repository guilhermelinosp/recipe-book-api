using FluentValidation;
using FluentValidation.Results;
using RecipeBook.Domain.Dtos.Requests.Recipes;
using RecipeBook.Exceptions;

namespace RecipeBook.Application.UseCases.Recipes.UpdateRecipe;

public class UpdateRecipeValidator : AbstractValidator<UpdateRecipeRequest>
{
    public UpdateRecipeValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(ErrorMessages.RECEITA_TITULO_NAO_INFORMADO);

        RuleFor(x => x.Category)
            .IsInEnum()
            .WithMessage(ErrorMessages.RECEITA_CATEGORIA_INVALIDO);

        RuleFor(x => x.PreparationMode)
            .NotEmpty()
            .WithMessage(ErrorMessages.RECEITA_MODOPREPARO_RECEITA_NAO_INFORMADO);

        RuleFor(x => x.PreparationTime)
            .NotEmpty()
            .WithMessage(ErrorMessages.RECEITA_TEMPOPREPARO_NAO_INFORMADO);

        RuleFor(x => x.Ingredients)
            .NotEmpty()
            .WithMessage(ErrorMessages.RECEITA_INGREDIENTE_NAO_INFORMADO);

        RuleForEach(x => x.Ingredients)
            .ChildRules(i =>
            {
                i.RuleFor(x => x.Product)
                    .NotEmpty()
                    .WithMessage(ErrorMessages.INGREDIENTE_PRODUTO_NAO_INFORMADO);
                i.RuleFor(x => x.Quantity)
                    .NotEmpty()
                    .WithMessage(ErrorMessages.INGREDIENTE_QUANTIDADE_NAO_INFORMADA);
            });

        RuleFor(x => x.Ingredients).Custom((ingredientes, contexto) =>
        {
            if (ingredientes.Select(c => c.Product.ToLowerInvariant()).Distinct(StringComparer.OrdinalIgnoreCase)
                    .Count() != ingredientes.Count)
                contexto.AddFailure(new ValidationFailure("Ingredientes",
                    ErrorMessages.RECEITA_INGREDIENTES_REPETIDOS));
        });
    }
}
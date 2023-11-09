using AutoMapper;
using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Domain.Dtos.Requests.Ingredients;
using RecipeBook.Domain.Dtos.Requests.Recipes;
using RecipeBook.Domain.Dtos.Responses.Account;
using RecipeBook.Domain.Dtos.Responses.Ingredients;
using RecipeBook.Domain.Dtos.Responses.Recipes;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Services.AutoMapper;

internal class AppAutoMapper : Profile
{
    public AppAutoMapper()
    {
        CreateMap<SignUpRequest, Account>();
        CreateMap<CreateRecipeRequest, Recipe>();
        CreateMap<IngredientRequest, Ingredient>();


        CreateMap<Recipe, RecipeResponse>();
        CreateMap<Ingredient, IngredientResponse>();
        CreateMap<Account, AccountResponse>();
    }
}

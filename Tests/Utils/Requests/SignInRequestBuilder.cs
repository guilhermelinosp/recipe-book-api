using Bogus;
using RecipeBook.Domain.Dtos.Requests;

namespace Utils.Requests;

public static class SignInRequestBuilder
{
    public static SignInRequest Build(int passwordSize = 8)
    {
        return new Faker<SignInRequest>()
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Password, f => f.Internet.Password(passwordSize));
    }
}
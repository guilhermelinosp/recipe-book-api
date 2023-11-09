using Bogus;
using RecipeBook.Domain.Dtos.Requests.Account;

namespace Utils.Requests;

public static class SignInBuilderRequest
{
    public static SignInRequest Build(int passwordSize = 8)
    {
        return new Faker<SignInRequest>()
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Password, f => f.Internet.Password(passwordSize));
    }
}
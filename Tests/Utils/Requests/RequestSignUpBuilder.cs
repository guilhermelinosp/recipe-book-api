using Bogus;
using RecipeBook.Domain.Dtos.Requests;

namespace Utils.Test.Requests;

public static class RequestSignUpBuilder
{
    public static RequestSignUp Build(int passwordSize = 8)
    {
        return new Faker<RequestSignUp>()
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Password, f => f.Internet.Password(passwordSize))
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("##!########").Replace("!", "9"));
    }
}
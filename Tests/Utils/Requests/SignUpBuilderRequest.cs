using Bogus;
using RecipeBook.Domain.Dtos.Requests;

namespace Utils.Requests;

public static class SignUpBuilderRequest
{
    public static SignUpRequest Build(int passwordSize = 9)
    {
        return new Faker<SignUpRequest>()
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Password, f => f.Internet.Password(passwordSize))
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("##!########").Replace("!", "9"));
    }
}
using Moq;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;

namespace Utils.Test.Repositories;

public class UserRepositoryBuilder
{
    private static UserRepositoryBuilder? _instance;
    private readonly Mock<IUserRepository>? _mock;

    private UserRepositoryBuilder()
    {
        _mock ??= new Mock<IUserRepository>();
    }

    public static UserRepositoryBuilder? Instance()
    {
        _instance = new UserRepositoryBuilder();
        return _instance;
    }

    public IUserRepository Build()
    {
        return _mock!.Object;
    }

    public UserRepositoryBuilder WithExistsByEmail(string email)
    {
        var user = new User();
        _mock!.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);
        return this;
    }

    public UserRepositoryBuilder WithExistsByPhone(string phone)
    {
        var user = new User();
        _mock!.Setup(x => x.GetByPhoneAsync(phone)).ReturnsAsync(user);
        return this;
    }
}
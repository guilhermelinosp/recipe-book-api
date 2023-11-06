using Moq;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;

namespace Utils.Repositories;

public class AccountRepositoryBuilder
{
    private static AccountRepositoryBuilder? _instance;
    private readonly Mock<IAccountRepository>? _mock;

    private AccountRepositoryBuilder()
    {
        _mock ??= new Mock<IAccountRepository>();
    }

    public static AccountRepositoryBuilder? Instance()
    {
        _instance = new AccountRepositoryBuilder();
        return _instance;
    }

    public IAccountRepository Build()
    {
        return _mock!.Object;
    }

    public AccountRepositoryBuilder WithExistsByEmail(SignInRequest user)
    {
        _mock!.Setup(x => x.GetByEmailAsync(user.Email!)).ReturnsAsync(new Account());
        return this;
    }

    public AccountRepositoryBuilder WithExistsByPhone(Account user)
    {
        _mock!.Setup(x => x.GetByPhoneAsync(user.Phone!)).ReturnsAsync(user);
        return this;
    }

}
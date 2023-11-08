using Moq;
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
}
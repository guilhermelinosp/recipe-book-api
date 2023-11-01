using AutoMapper;
using Moq;

namespace Utils.AutoMapper;

public class AutoMapperBuilder
{
    private static AutoMapperBuilder? _instance;
    private readonly Mock<IMapper>? _mock;

    private AutoMapperBuilder()
    {
        _mock ??= new Mock<IMapper>();
    }

    public static AutoMapperBuilder? Instance()
    {
        _instance = new AutoMapperBuilder();
        return _instance;
    }

    public IMapper Build()
    {
        return _mock!.Object;
    }
}
using FluentAssertions;
using System.Net;
using Utils.Requests;
using WebApi.Test.Controllers;
using Xunit;

namespace WebApi.Test.Users.SignUp;

public class SignUpWebApiUnitTest : ControllerBase
{
    public SignUpWebApiUnitTest(WebApiFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task ShouldBeCreatedRequest()
    {
        var request = SignUpRequestBuilder.Build();

        request.Password = "Password@123";

        var response = await PostRequest("api/user/sign-up", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task ShouldBeBadRequest()
    {
        var request = SignUpRequestBuilder.Build();

        request.Name = string.Empty;

        var response = await PostRequest("api/user/sign-up", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
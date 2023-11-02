using RecipeBook.Exceptions;
using System.Globalization;
using System.Net.Http.Json;
using Xunit;

namespace WebApi.Test.Controllers;

public class ControllerBase : IClassFixture<WebApiFactory<Program>>
{
    private readonly WebApiFactory<Program> _factory;
    private readonly HttpClient _client;

    private ControllerBase(WebApiFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        ErrorMessages.Culture = CultureInfo.CurrentCulture;
    }

    protected async Task<HttpResponseMessage> PostRequest(string method, object body)
    {
        return await _client.PostAsJsonAsync(method, body);
    }


}
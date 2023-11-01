using RecipeBook.Exceptions;
using System.Globalization;
using System.Net.Http.Json;
using Xunit;

namespace WebApi.Test.Controllers;

public class ControllerBase : IClassFixture<WebApiFactory<Program>>
{
    private readonly HttpClient _client;

    public ControllerBase(WebApiFactory<Program> factory)
    {
        _client = factory.CreateClient();
        ErrorMessages.Culture = CultureInfo.CurrentCulture;
    }

    protected async Task<HttpResponseMessage> PostRequest(string method, object body)
    {
        return await _client.PostAsJsonAsync(method, body);
    }


}
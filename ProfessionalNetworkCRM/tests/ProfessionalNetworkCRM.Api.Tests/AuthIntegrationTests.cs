using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProfessionalNetworkCRM.Api.Features.Auth;
using ProfessionalNetworkCRM.Api.Features.Contacts;
using ProfessionalNetworkCRM.Core;

namespace ProfessionalNetworkCRM.Api.Tests;

[TestFixture]
public class AuthIntegrationTests
{
    [Test]
    public async Task Login_WithTenantHeader_AndValidCredentials_ReturnsToken()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        client.DefaultRequestHeaders.Add("X-Tenant-Id", Constants.DefaultTenantId.ToString());

        var response = await client.PostAsJsonAsync("/api/auth/login", new LoginCommand
        {
            Username = "dev",
            Password = "DevPassword123!",
        });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<LoginResult>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Token, Is.Not.Empty);
        Assert.That(result.User.UserName, Is.EqualTo("dev"));
    }

    [Test]
    public async Task Contacts_WithTenantHeader_AreFilteredByTenantQueryFilter()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        client.DefaultRequestHeaders.Add("X-Tenant-Id", Constants.DefaultTenantId.ToString());

        var response = await client.GetAsync("/api/contacts");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        jsonOptions.Converters.Add(new JsonStringEnumConverter());
        var contacts = await response.Content.ReadFromJsonAsync<List<ContactDto>>(jsonOptions);
        Assert.That(contacts, Is.Not.Null);

        // Seeded database contains 1 contact in DefaultTenantId and 1 contact in another tenant.
        Assert.That(contacts!, Has.Count.EqualTo(1));
        Assert.That(contacts[0].FirstName, Is.EqualTo("Dev"));
    }

    [Test]
    public async Task Login_WithoutTenantHeader_InProduction_ReturnsUnauthorized()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/auth/login", new LoginCommand
        {
            Username = "dev",
            Password = "DevPassword123!",
        });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Login_WithWrongTenantHeader_InProduction_ReturnsUnauthorized()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        // User is seeded under Constants.DefaultTenantId, so a different tenant must not be able to see them.
        client.DefaultRequestHeaders.Add("X-Tenant-Id", Guid.Parse("aaaaaaaa-1111-1111-1111-111111111111").ToString());

        var response = await client.PostAsJsonAsync("/api/auth/login", new LoginCommand
        {
            Username = "dev",
            Password = "DevPassword123!",
        });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
}

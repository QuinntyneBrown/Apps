using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProfessionalNetworkCRM.Api.Features.Opportunities;
using ProfessionalNetworkCRM.Api.Features.Introductions;
using ProfessionalNetworkCRM.Api.Features.Referrals;
using ProfessionalNetworkCRM.Core;
using ProfessionalNetworkCRM.Core.Models.OpportunityAggregate.Enums;
using ProfessionalNetworkCRM.Core.Models.IntroductionAggregate.Enums;

namespace ProfessionalNetworkCRM.Api.Tests;

[TestFixture]
public class OpportunityTrackingIntegrationTests
{
    private JsonSerializerOptions GetJsonOptions()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new JsonStringEnumConverter());
        return options;
    }

    [Test]
    public async Task CreateOpportunity_WithValidData_ReturnsCreated()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        client.DefaultRequestHeaders.Add("X-Tenant-Id", Constants.DefaultTenantId.ToString());

        var command = new CreateOpportunityCommand
        {
            ContactId = Guid.NewGuid(),
            OpportunityType = OpportunityType.Job,
            Description = "Software Engineer Position",
            PotentialValue = "120000",
            Status = OpportunityStatus.Identified,
            Notes = "Test opportunity",
        };

        var response = await client.PostAsJsonAsync("/api/opportunities", command);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        var result = await response.Content.ReadFromJsonAsync<OpportunityDto>(GetJsonOptions());
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.OpportunityId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(result.Description, Is.EqualTo("Software Engineer Position"));
        Assert.That(result.Type, Is.EqualTo(OpportunityType.Job));
        Assert.That(result.Status, Is.EqualTo(OpportunityStatus.Identified));
        Assert.That(result.Value, Is.EqualTo(120000m));
    }

    [Test]
    public async Task GetOpportunities_ReturnsAllOpportunities()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        client.DefaultRequestHeaders.Add("X-Tenant-Id", Constants.DefaultTenantId.ToString());

        // Create an opportunity first
        var createCommand = new CreateOpportunityCommand
        {
            ContactId = Guid.NewGuid(),
            OpportunityType = OpportunityType.Client,
            Description = "New Client Project",
            Status = OpportunityStatus.Pursuing,
        };

        await client.PostAsJsonAsync("/api/opportunities", createCommand);

        // Get all opportunities
        var response = await client.GetAsync("/api/opportunities");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var opportunities = await response.Content.ReadFromJsonAsync<List<OpportunityDto>>(GetJsonOptions());
        Assert.That(opportunities, Is.Not.Null);
        Assert.That(opportunities!.Count, Is.GreaterThanOrEqualTo(1));
    }

    [Test]
    public async Task UpdateOpportunity_WithValidData_ReturnsUpdated()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        client.DefaultRequestHeaders.Add("X-Tenant-Id", Constants.DefaultTenantId.ToString());

        // Create an opportunity first
        var createCommand = new CreateOpportunityCommand
        {
            ContactId = Guid.NewGuid(),
            OpportunityType = OpportunityType.Partnership,
            Description = "Partnership Opportunity",
            Status = OpportunityStatus.Identified,
        };

        var createResponse = await client.PostAsJsonAsync("/api/opportunities", createCommand);
        var created = await createResponse.Content.ReadFromJsonAsync<OpportunityDto>(GetJsonOptions());

        // Update the opportunity
        var updateCommand = new UpdateOpportunityCommand
        {
            OpportunityId = created!.OpportunityId,
            Description = "Updated Partnership Opportunity",
            Status = OpportunityStatus.Won,
            PotentialValue = "50000",
            Notes = "Successfully closed the deal",
        };

        var response = await client.PutAsJsonAsync($"/api/opportunities/{created.OpportunityId}", updateCommand);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<OpportunityDto>(GetJsonOptions());
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Description, Is.EqualTo("Updated Partnership Opportunity"));
        Assert.That(result.Status, Is.EqualTo(OpportunityStatus.Won));
        Assert.That(result.Value, Is.EqualTo(50000m));
    }

    [Test]
    public async Task DeleteOpportunity_WithValidId_ReturnsNoContent()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        client.DefaultRequestHeaders.Add("X-Tenant-Id", Constants.DefaultTenantId.ToString());

        // Create an opportunity first
        var createCommand = new CreateOpportunityCommand
        {
            ContactId = Guid.NewGuid(),
            OpportunityType = OpportunityType.Speaking,
            Description = "Conference Speaking Opportunity",
            Status = OpportunityStatus.Identified,
        };

        var createResponse = await client.PostAsJsonAsync("/api/opportunities", createCommand);
        var created = await createResponse.Content.ReadFromJsonAsync<OpportunityDto>(GetJsonOptions());

        // Delete the opportunity
        var response = await client.DeleteAsync($"/api/opportunities/{created!.OpportunityId}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

        // Verify it's deleted
        var getResponse = await client.GetAsync($"/api/opportunities/{created.OpportunityId}");
        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task RequestIntroduction_WithValidData_ReturnsCreated()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        client.DefaultRequestHeaders.Add("X-Tenant-Id", Constants.DefaultTenantId.ToString());

        var command = new RequestIntroductionCommand
        {
            FromContactId = Guid.NewGuid(),
            ToContactId = Guid.NewGuid(),
            Purpose = "To discuss potential collaboration",
            Notes = "Both work in the same industry",
        };

        var response = await client.PostAsJsonAsync("/api/introductions/request", command);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        var result = await response.Content.ReadFromJsonAsync<IntroductionDto>(GetJsonOptions());
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.IntroductionId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(result.Purpose, Is.EqualTo("To discuss potential collaboration"));
        Assert.That(result.Status, Is.EqualTo(IntroductionStatus.Requested));
    }

    [Test]
    public async Task MakeIntroduction_WithValidId_UpdatesStatus()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        client.DefaultRequestHeaders.Add("X-Tenant-Id", Constants.DefaultTenantId.ToString());

        // Request an introduction first
        var requestCommand = new RequestIntroductionCommand
        {
            FromContactId = Guid.NewGuid(),
            ToContactId = Guid.NewGuid(),
            Purpose = "Business networking",
        };

        var requestResponse = await client.PostAsJsonAsync("/api/introductions/request", requestCommand);
        var requested = await requestResponse.Content.ReadFromJsonAsync<IntroductionDto>(GetJsonOptions());

        // Make the introduction
        var makeCommand = new MakeIntroductionCommand
        {
            IntroductionId = requested!.IntroductionId,
            Notes = "Introduction email sent",
        };

        var response = await client.PostAsJsonAsync("/api/introductions/make", makeCommand);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        var result = await response.Content.ReadFromJsonAsync<IntroductionDto>(GetJsonOptions());
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Status, Is.EqualTo(IntroductionStatus.Made));
        Assert.That(result.Notes, Is.EqualTo("Introduction email sent"));
    }

    [Test]
    public async Task CreateReferral_WithValidData_ReturnsCreated()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        client.DefaultRequestHeaders.Add("X-Tenant-Id", Constants.DefaultTenantId.ToString());

        var command = new CreateReferralCommand
        {
            SourceContactId = Guid.NewGuid(),
            Description = "Referral for consulting project",
            Outcome = "Meeting scheduled",
            Notes = "High priority referral",
        };

        var response = await client.PostAsJsonAsync("/api/referrals", command);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        var result = await response.Content.ReadFromJsonAsync<ReferralDto>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.ReferralId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(result.Description, Is.EqualTo("Referral for consulting project"));
        Assert.That(result.ThankYouSent, Is.False);
    }

    [Test]
    public async Task UpdateReferral_MarkThankYouSent_UpdatesFlag()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        client.DefaultRequestHeaders.Add("X-Tenant-Id", Constants.DefaultTenantId.ToString());

        // Create a referral first
        var createCommand = new CreateReferralCommand
        {
            SourceContactId = Guid.NewGuid(),
            Description = "Job referral",
        };

        var createResponse = await client.PostAsJsonAsync("/api/referrals", createCommand);
        var created = await createResponse.Content.ReadFromJsonAsync<ReferralDto>(GetJsonOptions());

        // Update the referral
        var updateCommand = new UpdateReferralCommand
        {
            ReferralId = created!.ReferralId,
            Outcome = "Hired!",
            ThankYouSent = true,
            Notes = "Sent thank you card",
        };

        var response = await client.PutAsJsonAsync($"/api/referrals/{created.ReferralId}", updateCommand);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<ReferralDto>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.ThankYouSent, Is.True);
        Assert.That(result.Outcome, Is.EqualTo("Hired!"));
    }

    [Test]
    public async Task GetReferrals_WithFilter_ReturnsFilteredResults()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();

        client.DefaultRequestHeaders.Add("X-Tenant-Id", Constants.DefaultTenantId.ToString());

        var contactId = Guid.NewGuid();

        // Create referrals
        await client.PostAsJsonAsync("/api/referrals", new CreateReferralCommand
        {
            SourceContactId = contactId,
            Description = "First referral",
        });

        await client.PostAsJsonAsync("/api/referrals", new CreateReferralCommand
        {
            SourceContactId = Guid.NewGuid(),
            Description = "Second referral",
        });

        // Get filtered referrals
        var response = await client.GetAsync($"/api/referrals?sourceContactId={contactId}");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var referrals = await response.Content.ReadFromJsonAsync<List<ReferralDto>>();
        Assert.That(referrals, Is.Not.Null);
        Assert.That(referrals!.Count, Is.EqualTo(1));
        Assert.That(referrals[0].SourceContactId, Is.EqualTo(contactId));
    }
}

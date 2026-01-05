using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProfessionalNetworkCRM.Core;
using ProfessionalNetworkCRM.Core.Models.UserAggregate;
using ProfessionalNetworkCRM.Core.Services;
using ProfessionalNetworkCRM.Infrastructure;

namespace ProfessionalNetworkCRM.Api.Tests;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(Environments.Production);

        builder.ConfigureServices(services =>
        {
            // Replace SQL Server DB with an in-memory Sqlite database for integration tests.
            RemoveService<DbContextOptions<ProfessionalNetworkCRMContext>>(services);
            RemoveService<ProfessionalNetworkCRMContext>(services);
            RemoveService<IProfessionalNetworkCRMContext>(services);

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            services.AddSingleton(connection);

            services.AddDbContext<ProfessionalNetworkCRMContext>((sp, options) =>
            {
                var sqlite = sp.GetRequiredService<SqliteConnection>();
                options.UseSqlite(sqlite);
            });

            services.AddScoped<IProfessionalNetworkCRMContext>(sp =>
                sp.GetRequiredService<ProfessionalNetworkCRMContext>());

            // Build the provider so we can create/seed the database.
            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProfessionalNetworkCRMContext>();

            context.Database.EnsureCreated();

            SeedTestData(context, scope.ServiceProvider.GetRequiredService<IPasswordHasher>());
        });
    }

    private static void SeedTestData(ProfessionalNetworkCRMContext context, IPasswordHasher passwordHasher)
    {
        // Seed a tenant-scoped user for login tests.
        var (hashed, salt) = passwordHasher.HashPassword("DevPassword123!");
        var user = new User(Constants.DefaultTenantId, "dev", "dev@professionalnetworkcrm.local", hashed, salt);
        context.Users.Add(user);

        // Seed two contacts in two different tenants to validate header-driven query filters.
        var devContact = new Contact
        {
            ContactId = Guid.NewGuid(),
            TenantId = Constants.DefaultTenantId,
            UserId = user.UserId,
            FirstName = "Dev",
            LastName = "Tenant",
            ContactType = ContactType.Colleague,
            DateMet = DateTime.UtcNow.Date,
            CreatedAt = DateTime.UtcNow,
            IsPriority = false,
            Tags = new List<string> { "dev" },
        };

        var otherTenantId = Guid.Parse("aaaaaaaa-1111-1111-1111-111111111111");
        var otherContact = new Contact
        {
            ContactId = Guid.NewGuid(),
            TenantId = otherTenantId,
            UserId = Guid.NewGuid(),
            FirstName = "Other",
            LastName = "Tenant",
            ContactType = ContactType.Client,
            DateMet = DateTime.UtcNow.Date,
            CreatedAt = DateTime.UtcNow,
            IsPriority = true,
            Tags = new List<string> { "other" },
        };

        context.Contacts.AddRange(devContact, otherContact);
        context.SaveChanges();
    }

    private static void RemoveService<T>(IServiceCollection services)
    {
        var matches = services.Where(d => d.ServiceType == typeof(T)).ToList();
        foreach (var descriptor in matches)
        {
            services.Remove(descriptor);
        }
    }
}

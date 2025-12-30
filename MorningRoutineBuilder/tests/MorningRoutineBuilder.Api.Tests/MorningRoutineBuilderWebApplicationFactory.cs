// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MorningRoutineBuilder.Core;
using MorningRoutineBuilder.Infrastructure;

namespace MorningRoutineBuilder.Api.Tests;

/// <summary>
/// Custom WebApplicationFactory for integration testing.
/// </summary>
public class MorningRoutineBuilderWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<MorningRoutineBuilderContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add DbContext using in-memory database for testing
            services.AddDbContext<MorningRoutineBuilderContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            services.AddScoped<IMorningRoutineBuilderContext>(provider =>
                provider.GetRequiredService<MorningRoutineBuilderContext>());

            // Build the service provider
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<MorningRoutineBuilderContext>();

            // Ensure the database is created
            db.Database.EnsureCreated();
        });
    }
}

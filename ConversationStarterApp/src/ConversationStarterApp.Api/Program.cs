// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Api;
using ConversationStarterApp.Infrastructure;
using ConversationStarterApp.Infrastructure.Data;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithEnvironmentName()
        .Enrich.WithMachineName()
        .Enrich.WithThreadId()
        .WriteTo.Console();
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ConversationStarterApp API",
        Version = "v1",
        Description = "API for managing conversation starter prompts, sessions, and favorites",
    });
});

// Add MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Add Infrastructure services
builder.Services.AddInfrastructureServices(builder.Configuration);

// Configure CORS
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:4200" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

// Seed the database in development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ConversationStarterAppContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    // Ensure database is created and seeded
    await context.Database.EnsureCreatedAsync();

    // Seed data if database is empty
    if (!context.Prompts.Any())
    {
        var sampleUserId = Guid.NewGuid();
        var prompts = SeedData.GetSamplePrompts(sampleUserId).ToList();
        context.Prompts.AddRange(prompts);

        var favorites = SeedData.GetSampleFavorites(sampleUserId, prompts[0].PromptId);
        context.Favorites.AddRange(favorites);

        var sessions = SeedData.GetSampleSessions(sampleUserId);
        context.Sessions.AddRange(sessions);

        await context.SaveChangesAsync();
        logger.LogInformation("Database seeded successfully");
    }
}

app.Run();

/// <summary>
/// Program class for test access.
/// </summary>
public partial class Program
{
}

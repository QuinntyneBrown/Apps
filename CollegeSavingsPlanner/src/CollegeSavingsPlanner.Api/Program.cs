using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CollegeSavingsPlanner.Infrastructure;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    Log.Information("Starting College Savings Planner API");

    // Add services to the container.
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "College Savings Planner API", Version = "v1" });
    });

    // Add Infrastructure services (DbContext)
    builder.Services.AddInfrastructureServices(builder.Configuration);
// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? "YourSuperSecretKeyThatIsAtLeast32CharactersLong!");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"] ?? "YourApp",
        ValidAudience = jwtSettings["Audience"] ?? "YourApp",
        IssuerSigningKey = new SymmetricSecurityKey(key),
    };
});

builder.Services.AddAuthorization();


    // Add MediatR
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

    // Add CORS
    var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:4200" };
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigins",
            policy =>
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
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "College Savings Planner API v1"));
    }

    app.UseSerilogRequestLogging();

    
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

    app.UseCors("AllowSpecificOrigins");

    app.UseAuthorization();

    app.MapControllers();

    Log.Information("College Savings Planner API started successfully");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

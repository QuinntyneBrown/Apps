var builder = DistributedApplication.CreateBuilder(args);

// Add infrastructure services
var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("ResumeCareerAchievementTrackerDb");

var rabbitMq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();

// Add microservices
var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var achievementsApi = builder.AddProject<Projects.Achievements_Api>("achievements-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var skillsApi = builder.AddProject<Projects.Skills_Api>("skills-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var projectsApi = builder.AddProject<Projects.Projects_Api>("projects-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

// Add infrastructure services
var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("FocusSessionTrackerDb");

var rabbitMq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();

// Add microservices
var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var distractionsApi = builder.AddProject<Projects.Distractions_Api>("distractions-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var focusSessionsApi = builder.AddProject<Projects.FocusSessions_Api>("focussessions-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var sessionAnalyticsApi = builder.AddProject<Projects.SessionAnalytics_Api>("sessionanalytics-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

builder.Build().Run();

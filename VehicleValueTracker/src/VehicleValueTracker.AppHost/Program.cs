var builder = DistributedApplication.CreateBuilder(args);

// Add infrastructure services
var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("VehicleValueTrackerDb");

var rabbitMq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();

// Add microservices
var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var vehiclesApi = builder.AddProject<Projects.Vehicles_Api>("vehicles-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var valueAssessmentsApi = builder.AddProject<Projects.ValueAssessments_Api>("valueassessments-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var marketComparisonsApi = builder.AddProject<Projects.MarketComparisons_Api>("marketcomparisons-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

builder.Build().Run();

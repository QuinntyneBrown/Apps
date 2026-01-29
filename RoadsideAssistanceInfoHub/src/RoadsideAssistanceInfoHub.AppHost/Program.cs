var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql").AddDatabase("RoadsideAssistanceInfoHubDb");
var rabbitMq = builder.AddRabbitMQ("messaging").WithManagementPlugin();

var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var vehiclesApi = builder.AddProject<Projects.Vehicles_Api>("vehicles-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var insuranceApi = builder.AddProject<Projects.Insurance_Api>("insurance-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var contactsApi = builder.AddProject<Projects.Contacts_Api>("contacts-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var policiesApi = builder.AddProject<Projects.Policies_Api>("policies-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

builder.Build().Run();

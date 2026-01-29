var builder = DistributedApplication.CreateBuilder(args);

// Add infrastructure services
var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("ProfessionalReadingListDb");

var rabbitMq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();

// Add microservices
var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var resourcesApi = builder.AddProject<Projects.Resources_Api>("resources-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var readingProgressApi = builder.AddProject<Projects.ReadingProgress_Api>("readingprogress-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var notesApi = builder.AddProject<Projects.Notes_Api>("notes-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("FreelanceProjectManagerDb");

var rabbitMq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();

var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var clientsApi = builder.AddProject<Projects.Clients_Api>("clients-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var projectsApi = builder.AddProject<Projects.Projects_Api>("projects-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var invoicesApi = builder.AddProject<Projects.Invoices_Api>("invoices-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var timeEntriesApi = builder.AddProject<Projects.TimeEntries_Api>("timeentries-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

builder.Build().Run();

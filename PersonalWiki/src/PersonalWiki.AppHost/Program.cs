var builder = DistributedApplication.CreateBuilder(args);

// Add infrastructure services
var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("PersonalWikiDb");

var rabbitMq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();

// Add microservices
var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var pagesApi = builder.AddProject<Projects.Pages_Api>("pages-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var categoriesApi = builder.AddProject<Projects.Categories_Api>("categories-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var linksApi = builder.AddProject<Projects.Links_Api>("links-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var revisionsApi = builder.AddProject<Projects.Revisions_Api>("revisions-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql").AddDatabase("FamilyTreeBuilderDb");
var rabbitMq = builder.AddRabbitMQ("messaging").WithManagementPlugin();

var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api")
    .WithReference(sqlServer).WithReference(rabbitMq);

var personsApi = builder.AddProject<Projects.Persons_Api>("persons-api")
    .WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);

var mediaApi = builder.AddProject<Projects.Media_Api>("media-api")
    .WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);

builder.Build().Run();

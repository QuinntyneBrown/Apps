var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("CampingTripPlannerDb");

var rabbitMq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();

var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var campsitesApi = builder.AddProject<Projects.Campsites_Api>("campsites-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var gearChecklistsApi = builder.AddProject<Projects.GearChecklists_Api>("gearchecklists-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var reviewsApi = builder.AddProject<Projects.Reviews_Api>("reviews-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var tripsApi = builder.AddProject<Projects.Trips_Api>("trips-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

builder.Build().Run();

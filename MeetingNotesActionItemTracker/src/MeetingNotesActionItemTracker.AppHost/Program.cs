var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("MeetingNotesActionItemTrackerDb");

var rabbitMq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();

var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var meetingsApi = builder.AddProject<Projects.Meetings_Api>("meetings-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var notesApi = builder.AddProject<Projects.Notes_Api>("notes-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var actionItemsApi = builder.AddProject<Projects.ActionItems_Api>("actionitems-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

builder.Build().Run();

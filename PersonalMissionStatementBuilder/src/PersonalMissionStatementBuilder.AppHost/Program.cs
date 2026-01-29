var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("PersonalMissionStatementBuilderDb");

var rabbitMq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();

var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq);

var valuesApi = builder.AddProject<Projects.Values_Api>("values-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi);

var missionStatementsApi = builder.AddProject<Projects.MissionStatements_Api>("missionstatements-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi)
    .WithReference(valuesApi);

var goalsApi = builder.AddProject<Projects.Goals_Api>("goals-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi)
    .WithReference(missionStatementsApi);

var progressApi = builder.AddProject<Projects.Progress_Api>("progress-api")
    .WithReference(sqlServer)
    .WithReference(rabbitMq)
    .WithReference(identityApi)
    .WithReference(goalsApi);

builder.Build().Run();

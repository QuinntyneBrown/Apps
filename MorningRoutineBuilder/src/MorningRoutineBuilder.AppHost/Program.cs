var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql").AddDatabase("MorningRoutineBuilderDb");
var rabbitMq = builder.AddRabbitMQ("messaging").WithManagementPlugin();

var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api").WithReference(sqlServer).WithReference(rabbitMq);
var routinesApi = builder.AddProject<Projects.Routines_Api>("routines-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);
var tasksApi = builder.AddProject<Projects.Tasks_Api>("tasks-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);
var streaksApi = builder.AddProject<Projects.Streaks_Api>("streaks-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);
var completionLogsApi = builder.AddProject<Projects.CompletionLogs_Api>("completionlogs-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);
var sqlServer = builder.AddSqlServer("sql").AddDatabase("WeeklyReviewSystemDb");
var rabbitMq = builder.AddRabbitMQ("messaging").WithManagementPlugin();
var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api").WithReference(sqlServer).WithReference(rabbitMq);
var reviewsApi = builder.AddProject<Projects.Reviews_Api>("reviews-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);
var prioritiesApi = builder.AddProject<Projects.Priorities_Api>("priorities-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);
var accomplishmentsApi = builder.AddProject<Projects.Accomplishments_Api>("accomplishments-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);
var challengesApi = builder.AddProject<Projects.Challenges_Api>("challenges-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);
builder.Build().Run();

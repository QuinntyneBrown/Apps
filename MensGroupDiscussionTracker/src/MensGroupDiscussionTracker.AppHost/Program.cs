var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql").AddDatabase("MensGroupDiscussionTrackerDb");
var rabbitMq = builder.AddRabbitMQ("messaging").WithManagementPlugin();

var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api").WithReference(sqlServer).WithReference(rabbitMq);
var groupsApi = builder.AddProject<Projects.Groups_Api>("groups-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);
var meetingsApi = builder.AddProject<Projects.Meetings_Api>("meetings-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);
var topicsApi = builder.AddProject<Projects.Topics_Api>("topics-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);
var resourcesApi = builder.AddProject<Projects.Resources_Api>("resources-api").WithReference(sqlServer).WithReference(rabbitMq).WithReference(identityApi);

builder.Build().Run();

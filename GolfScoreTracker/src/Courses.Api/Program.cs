using Courses.Core;
using Courses.Infrastructure.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddSqlServerDbContext<CoursesDbContext>("GolfScoreTrackerDb");
builder.Services.AddScoped<ICoursesDbContext>(p => p.GetRequiredService<CoursesDbContext>());
builder.AddRabbitMQClient("messaging");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();
app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

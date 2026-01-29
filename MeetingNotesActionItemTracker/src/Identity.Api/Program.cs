using Identity.Core;
using Identity.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddSqlServerDbContext<IdentityDbContext>("MeetingNotesActionItemTrackerDb");
builder.Services.AddScoped<IIdentityDbContext>(p => p.GetRequiredService<IdentityDbContext>());
builder.AddRabbitMQClient("messaging");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddIdentityServices();
builder.Services.AddCors(o => o.AddPolicy("AllowAll", p => p.WithOrigins("http://localhost:4200","http://localhost:3000","https://localhost:7001").AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
var app = builder.Build();
app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

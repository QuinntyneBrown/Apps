using Albums.Core;
using Albums.Infrastructure.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddSqlServerDbContext<AlbumsDbContext>("FamilyPhotoAlbumOrganizerDb");
builder.Services.AddScoped<IAlbumsDbContext>(p => p.GetRequiredService<AlbumsDbContext>());
builder.AddRabbitMQClient("messaging");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();
app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

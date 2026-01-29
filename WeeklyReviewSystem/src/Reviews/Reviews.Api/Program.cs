using Reviews.Core; using Reviews.Infrastructure.Data; using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args); builder.AddServiceDefaults();
builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddEndpointsApiExplorer(); builder.Services.AddSwaggerGen();
builder.AddSqlServerDbContext<ReviewsDbContext>("WeeklyReviewSystemDb");
builder.Services.AddScoped<IReviewsDbContext>(provider => provider.GetRequiredService<ReviewsDbContext>());
builder.AddRabbitMQClient("messaging"); builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddCors(options => { options.AddPolicy("AllowAll", policy => { policy.WithOrigins("http://localhost:4200", "http://localhost:3000", "https://localhost:7001").AllowAnyMethod().AllowAnyHeader().AllowCredentials(); }); });
var app = builder.Build(); app.MapDefaultEndpoints(); if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseHttpsRedirection(); app.UseCors("AllowAll"); app.UseAuthorization(); app.MapControllers(); app.Run();

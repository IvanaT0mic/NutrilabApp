using Nutrilab.Dtos.Startup;
using Nutrilab.Services.Startup;
using Nutrilab.Shared.Models;
using Nutrilab.WebApi.Extensions;
using Nutrilab.WebApi.Middlewares;
using Nutrilab.WebApi.Startup;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;
string connectionString = services.ConfigureConnectionString(configuration);

JwtSettings jwtSettings = builder.ConfigureJwtSettings();

services.ConfigureJwtAuth(jwtSettings);
services.ConfigureServices(connectionString);

services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();

services.ConfigureSwagger();
services.ConfigureCors();
services.StartDtoProject();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.Services.AutoUpdateDatabaseContextAsync();

app.UseHttpsRedirection();

app.UseCors("AllowPWA");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

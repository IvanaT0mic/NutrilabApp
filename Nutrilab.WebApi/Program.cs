using Nutrilab.Services.Startup;
using Nutrilab.Shared.Models;
using Nutrilab.WebApi.Extensions;
using Nutrilab.WebApi.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

var configuration = builder.Configuration;

string connectionString = services.ConfigureConnectionString(configuration);
JwtSettings jwtSettings = builder.ConfigureJwtSettings();
services.ConfigureJwtAuth(jwtSettings);
services.ConfigureServices(connectionString);

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.Services.AutoUpdateDatabaseContextAsync();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

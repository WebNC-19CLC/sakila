using Microsoft.EntityFrameworkCore;
using sakilaAppMySQL.Infrastructure.Context;
using sakilaAppMySQL.Infrastructure.Domain.Entities;
using sakilaAppMySQL.Infrastructure.Domain.Object.Configuration;
using sakilaAppMySQL.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// SETTINGS
var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings));
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();

//SERVICES
builder.Services.AddScoped<ActorService, ActorService>();
//DBContext
var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
builder.Services.AddDbContext<sakilaContext>(opt => opt.UseMySql(appSettings.SakilaConnectionString, serverVersion));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

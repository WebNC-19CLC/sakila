using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using sakilaAppMySQL.Hubs;
using sakilaAppMySQL.Infrastructure.Context;
using sakilaAppMySQL.Infrastructure.Domain.Entities.Authentication;
using sakilaAppMySQL.Infrastructure.Domain.Object.Configuration;
using sakilaAppMySQL.Infrastructure.Services;
using sakilaAppMySQL.Middlewares;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// SETTINGS
var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings));
builder.Services.Configure<AppSettings>(appSettingsSection);
ConfigurationManager configuration = builder.Configuration;
var appSettings = appSettingsSection.Get<AppSettings>();

//SERVICES
builder.Services.AddSignalR();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IFilmService, FilmService>();

builder.Services.AddAutoMapper((provider, opt) =>
{
  opt.ConstructServicesUsing(t => ActivatorUtilities.CreateInstance(provider, t));
}, Assembly.GetAssembly(typeof(Program)));
//DBContext
var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
builder.Services.AddDbContext<sakilaContext>(opt => opt.UseMySql(appSettings.SakilaConnectionString, serverVersion));

// For Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<sakilaContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = "JWT_OR_COOKIE";
  options.DefaultChallengeScheme = "JWT_OR_COOKIE";
  options.DefaultScheme = "JWT_OR_COOKIE";
})
.AddCookie("Cookies", options =>
{
  options.LoginPath = "/api/Authenticate/login";
  options.ExpireTimeSpan = TimeSpan.FromDays(1);
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ClockSkew = TimeSpan.Zero,

    ValidAudience = configuration["JWT:ValidAudience"],
    ValidIssuer = configuration["JWT:ValidIssuer"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
  };
})
.AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
{
  // runs on each request
  options.ForwardDefaultSelector = context =>
  {
    // filter by auth type
    string authorization = context.Request.Headers[HeaderNames.Authorization];
    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
      return "Bearer";

    // otherwise always check for cookie auth
    return "Cookies";
  };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "SakilaAPI", Version = "v1.0.1" });
  c.ExampleFilters();
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter token",
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    BearerFormat = "JWT",
    Scheme = "bearer"
  });
  c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

var configSettings = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configSettings)
    .CreateLogger();

builder.Host.UseSerilog();

//Enable CORS
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
  builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true);
}));



var app = builder.Build();

app.UseCors("corsapp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sakila API V1");
  });
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers();
  endpoints.MapHub<MessageHub>("notification");
});

app.MapControllers();

app.Run();

using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Services.AuthService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//---------------------------------------------------------------------------------------------
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddHttpContextAccessor();

// Here I add reference to the Entity Framework
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

    // When we are still in development, we can log some additional info that can help us
    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors(); // To get field-level error details
        options.EnableSensitiveDataLogging();
        options.ConfigureWarnings(warningsAction =>
        {
            warningsAction.Log(new EventId[]
            {
            CoreEventId.FirstWithoutOrderByAndFilterWarning,
            CoreEventId.RowLimitingOperationWithoutOrderByWarning
            });
        });
    }
});
//------------------------------------------------------------------

//=============================================================================================
// Adding my own dependency injection
builder.Services.AddScoped<IAuthService, AuthService>();

// Automating the Dependecy Injection process for every service I Add
//builder.Services.RegisterAssemblyTypes()

//=============================================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

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
using Serilog;
using EFDataAccessLibrary.Services.UsersService;
using EFDataAccessLibrary.Services.PostsService;
using EFDataAccessLibrary.Services.HobbiesService;
using EFDataAccessLibrary.Services.Followers2Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//---------------------------------------------------------------------------------------------
//Implementing a defferent logger than the default one
builder.Host.UseSerilog((context, config) => 
{
    config.WriteTo.Console();
});

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

// Here I add Cors Policy
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy" , opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

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
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddScoped<IHobbiesService, HobbiesService>();
builder.Services.AddScoped<IFollowersService, FollowersService>();
//=============================================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// To serve static images
app.UseStaticFiles();

// Configure to actually Use Cors
app.UseCors("OpenCorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

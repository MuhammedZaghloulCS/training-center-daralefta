using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Dependencies;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// -----------------------
// Logging Configuration
// -----------------------
builder.Logging.ClearProviders();          // Remove default providers (EventLog)
builder.Logging.AddConsole();              // Only use Console logging
builder.Logging.AddDebug();                // Optional debug logging

// -----------------------
// Controllers + JSON Options
// -----------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// -----------------------
// Swagger/OpenAPI
// -----------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -----------------------
// Database Contexts
// -----------------------
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<security_dbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecondConnection")));

// -----------------------
// JWT Authentication
// -----------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

// -----------------------
// Identity Configuration
// -----------------------
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
})
.AddRoles<IdentityRole<Guid>>()
.AddSignInManager()
.AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultTokenProviders();

// -----------------------
// HttpClient
// -----------------------
builder.Services.AddHttpClient("ExternalApi")
    .ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });

// -----------------------
// Infrastructure Dependencies
// -----------------------
builder.Services.AddedModuleInfraStructureDependencies();
builder.Services.AddedModuleApplicationDependencies();

// -----------------------
// CORS
// -----------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// -----------------------
// Mapster
// -----------------------
builder.Services.AddMapster();

// -----------------------
// Build app
// -----------------------
var app = builder.Build();

app.UseCors("AllowAll");

// -----------------------
// Middleware
// -----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // In production you can still enable Swagger if needed
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); // optional for HTTP

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
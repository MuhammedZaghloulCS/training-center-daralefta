using Application.Common.Abstraction;
using Application.Common.Implementation;
using Domain.Entities;
using Hangfire;
using Infrastructure.Context;
using Infrastructure.Dependencies;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// -----------------------
// Logging Configuration
// -----------------------
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// -----------------------
// Controllers + JSON Options
// -----------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddHangfire(x =>
    x.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), 
        new Hangfire.SqlServer.SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(30),
            QueuePollInterval = TimeSpan.FromSeconds(30), // Reduce polling frequency from default 1s
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));

builder.Services.AddHangfireServer(options =>
{
    options.WorkerCount = 2; // Limit concurrent workers
    options.ServerTimeout = TimeSpan.FromMinutes(5);
});

// -----------------------
// Swagger/OpenAPI
// -----------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "أدخل التوكن هنا. مثال: eyJhbGci..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// -----------------------
// Database Contexts
// -----------------------
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<security_dbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecondConnection")));

builder.Services.AddScoped<IFacePrintService, FacePrintService>();
builder.Services.AddScoped<HangfireJob>();

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
Console.WriteLine("KEY: " + builder.Configuration["Jwt:Key"]);
Console.WriteLine("ISSUER: " + builder.Configuration["Jwt:Issuer"]);
Console.WriteLine("AUDIENCE: " + builder.Configuration["Jwt:Audience"]);
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

// -----------------------
// Middleware
// -----------------------
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

// Serve static files from wwwroot (for user images)
app.UseStaticFiles();

app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    var backgroundJobClient = scope.ServiceProvider.GetRequiredService<IBackgroundJobClient>();

    backgroundJobClient.Enqueue<HangfireJob>(
        x => x.ProcessSessions()
    );

    recurringJobManager.AddOrUpdate<HangfireJob>(
        "assign-users-to-sessions",
        x => x.ProcessSessions(),
    Cron.Daily
    );
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await AdminSeeder.SeedAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the admin user.");
    }
}

app.UseHangfireDashboard("/dashboard");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

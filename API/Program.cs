using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Dependencies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDbContext<security_dbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SecondConnection"));
});


//JWT
// إعداد JWT Authentication
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

//Identity Dependencies
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

//HTTP Client
builder.Services.AddHttpClient("ExternalApi")
    .ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });
// Adding Infrastructure Dependencies
builder.Services.AddedModuleInfraStructureDependencies();

// Adding Infrastructure Dependencies
builder.Services.AddedModuleApplicationDependencies();

//CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Enable CORS
app.UseCors("CorsPolicy");

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


//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider
//        .GetRequiredService<RoleManager<IdentityRole<Guid>>>();

//    var userManager = scope.ServiceProvider
//        .GetRequiredService<UserManager<ApplicationUser>>();

//    await IdentitySeeder.SeedAsync(roleManager, userManager);
//}


app.Run();


using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Alabuga_API.Middleware;
using Alabuga_API.Persistens;
using Alabuga_API.Persistens.Repositories.Interfaces;
using Alabuga_API.Persistens.Repositories;
using Alabuga_API.Services;
using Alabuga_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// JWT Init
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JWT";
    options.DefaultChallengeScheme = "JWT";
})
.AddJwtBearer("JWT", options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:7048",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdwafatw"))
    };
})
.AddCookie("Cookies", options =>
{
    options.Cookie.Name = "auth-token";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

builder.Services.AddAuthorization();

// DB
builder.Services.AddDbContext<AlabugaContext>(
    options => { options.UseSqlite("Data Source=Alabuga.db"); }
);

// Dependency Injection
// --Repositories
builder.Services.AddScoped<IUserRepository, UserReposiotry>();
builder.Services.AddScoped<IMissionsRepository, MissionsRepository>();

// --Services
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IMissionsService, MissionsService>();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Type = SecuritySchemeType.Http
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtCookieMiddleware>();

app.MapControllers();

app.Run();
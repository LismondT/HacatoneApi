using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Alabuga_API.Models;
using Alabuga_API.Persistens;
using Alabuga_API.Persistens.Repositories;
using Alabuga_API.Persistens.Repositories.Interfaces;
using Alabuga_API.Services;
using Alabuga_API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// JWT Init
builder.Services.AddAuthentication().AddJwtBearer(options => options.TokenValidationParameters =
    new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:7048",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdwafatw"))
    });
builder.Services.AddAuthorization();

// DB
builder.Services.AddDbContext<AlabugaContext>(
    options =>
    {
        
    }
);

// Dependency Injection
// --Repositories
builder.Services.AddScoped<IUserRepository, UserReposiotry>();

// --Services
builder.Services.AddScoped<IJwtService, JwtService>();


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
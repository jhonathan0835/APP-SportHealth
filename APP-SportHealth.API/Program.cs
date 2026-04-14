using APP_SportHealth.API.Middlewares;
using APP_SportHealth.API.Validators;
using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Application.UseCases;
using APP_SportHealth.Infrastructure;
using APP_SportHealth.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Fluent Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();

// 🔹 DB PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// 🔹 Dependencias
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<CreateUserUseCase>();


// 🔹 JWT
var key = Encoding.UTF8.GetBytes("Flakito12345_super_secret_key_2026");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// 🔹 Build app
var app = builder.Build();

// 🔥 Swagger SIEMPRE activo (recomendado para desarrollo)
app.UseSwagger();
app.UseSwaggerUI();

// 🔹 Middleware
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
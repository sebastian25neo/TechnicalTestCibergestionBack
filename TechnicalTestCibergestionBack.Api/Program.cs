using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechnicalTestCibergestionBack.Application.Services;
using TechnicalTestCibergestionBack.Infrastructure.Repositories;
using TechnicalTestCibergestionBack.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using TechnicalTestCibergestionBack.Application.Validators;
using AutoMapper;
using TechnicalTestCibergestionBack.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// 1. Lee tu cadena de conexión del archivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Configura tu DbContext (si lo usas)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Registrar AutoMapper con todos los perfiles encontrados
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// 4. Inyecta la cadena de conexión para tus repositorios con lambdas
builder.Services.AddScoped<IUserRepository>(_ => new UserRepository(connectionString));
builder.Services.AddScoped<ISurveyRepository>(_ => new SurveyRepository(connectionString));

// 5. Registra los demás servicios
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// 6. Configura la autenticación JWT
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]);
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
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

// 7. Configurar controladores y FluentValidation
builder.Services
    .AddControllers()
    .AddFluentValidation(config =>
    {
        // Registra automáticamente todos los validadores del ensamblado de UserRegisterValidator
        config.RegisterValidatorsFromAssemblyContaining<UserRegisterValidator>();
        config.DisableDataAnnotationsValidation = true;
    });

// 8. Manejo global de errores de validación y JSON mal formado
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                e => e.Key,
                e => e.Value?.Errors.Select(err => err.ErrorMessage).ToArray()
            );

        var result = new
        {
            message = "Error en la solicitud. Verifique los datos enviados.",
            errors
        };

        return new BadRequestObjectResult(result);
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

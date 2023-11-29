using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecipeBook.API.Filters;
using RecipeBook.API.WebSockets;
using RecipeBook.Application;
using RecipeBook.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.EnvironmentName == "Development")
    builder.Configuration.AddUserSecrets<Program>();

var services = builder.Services;

services.AddSingleton<BroadcastHub>();
services.AddApplication();
services.AddInfrastructure(builder.Configuration);
services.AddHttpContextAccessor();
services.AddControllers();
services.AddHealthChecks();
services.AddEndpointsApiExplorer();
services.AddSignalR(opt => { opt.HandshakeTimeout = TimeSpan.FromMinutes(10); });
services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header utilizando o Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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

services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:5000", "ws://localhost:8000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});
services.AddMvc(opt => opt.Filters.Add(typeof(ExceptionFilter)));
services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.AddScoped<IAuthorizationHandler, AuthorizationHub>();
services.AddAuthorization(options =>
    options.AddPolicy("AuthorizationHub", policy => policy.Requirements.Add(new AuthorizationRequirement())));
services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt-Secret"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowLocalhost");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapHub<ConnectingHub>("ws/connecting");
app.MapHealthChecks("/health");

app.Run();
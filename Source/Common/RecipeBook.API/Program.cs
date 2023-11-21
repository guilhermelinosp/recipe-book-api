using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecipeBook.API.Filters;
using RecipeBook.API.WebSockets;
using RecipeBook.Application;
using RecipeBook.Infrastructure;
using RecipeBook.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

await Migratations();

builder
	.Services
	.AddApplication()
	.AddInfrastructure(builder.Configuration);

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.AddCors(opt =>
{
	opt.AddPolicy("*",
		b => b
			.AllowAnyOrigin());
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
	opt.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "1.0" });
	opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		In = ParameterLocation.Header,
		Description = "Enter 'Bearer' [space] and your token!"
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
builder.Services.AddHttpsRedirection(opt => opt.HttpsPort = 443);
builder.Services.AddMvc(opt => opt.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddSignalR();
builder.Services.AddAuthorization(option =>
{
	option.AddPolicy("AccountAuth", policy => policy.Requirements.Add(new AuthorizationRequirement()));
});builder.Services.AddScoped<IAuthorizationHandler, AuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationRequirement, AuthorizationRequirement>();
 
builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
	jwt.SaveToken = true;
	jwt.RequireHttpsMetadata = false;
	jwt.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt-Secret"]!)),
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		RequireExpirationTime = false	
	};
});
builder.Services.AddAuthorization();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	builder.Configuration.AddUserSecrets<Program>();
}
app.MapHealthChecks("/health", new HealthCheckOptions
{
	AllowCachingResponses = false,
	ResultStatusCodes =
	{
		[HealthStatus.Healthy] = StatusCodes.Status200OK,
		[HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
	}
});

app.MapHub<ConnectingHub>("/connecting");
app.UseRouting();
app.UseCors("*");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

async Task Migratations()
{
	var connectionString = builder.Configuration["ConnectionString"]!;
	await CreateTables.CreateTableAccountAsync(connectionString);
	await CreateTables.CreateTableRecipeAsync(connectionString);
	await CreateTables.CreateTableIngredientAsync(connectionString);
}
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecipeBook.API.Filters;
using RecipeBook.Application;
using RecipeBook.Infrastructure;
using RecipeBook.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

Migratations();

builder
	.Services
	.AddApplication()
	.AddInfrastructure();

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
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("Jwt-Secret")!)),
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
}

app.UseRouting();
app.UseCors("*");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

void Migratations()
{
	var connectionString = Environment.GetEnvironmentVariable("SQL-ConnectionString")!;
	Console.WriteLine($"Connection String: {connectionString}");

	CreateTables.CreateTableAccountAsync(connectionString);
	CreateTables.CreateTableRecipeAsync(connectionString);
	CreateTables.CreateTableIngredientAsync(connectionString);
}
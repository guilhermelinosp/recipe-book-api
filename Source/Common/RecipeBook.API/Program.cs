using RecipeBook.API.Filters;
using RecipeBook.Application;
using RecipeBook.Infrastructure;
using RecipeBook.Infrastructure.Persistence.Migrations;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

MigrateScrema();

builder
    .Services
    .AddApplication(configuration)
    .AddInfrastructure(configuration);

builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(opt => opt.Filters.Add(typeof(FilterException)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

return;

async void MigrateScrema()
{
    await Screma.CreateDatabaseAsync(configuration["ConnectionString"], configuration["Database"]);

    await Screma.CreateTablesAsync(configuration["ConnectionString"], configuration["Database"]);
}
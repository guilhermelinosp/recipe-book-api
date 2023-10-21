using RecipeBook.Infrastructure;
using RecipeBook.Infrastructure.Persistence.Migrations;

var builder = WebApplication.CreateBuilder(args);

MigrateScrema();

builder
    .Services
    .AddInfrastructure(builder.Configuration);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
    await Screma.CreateScremaAsync(builder.Configuration["ConnectionString"], builder.Configuration["Database"]);

    await Screma.CreateTablesAsync(builder.Configuration["ConnectionString"], builder.Configuration["Database"]);
}
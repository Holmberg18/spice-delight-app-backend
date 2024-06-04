using spice_delight_app_backend.Services;
using Microsoft.EntityFrameworkCore;
using spice_delight_app_backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Adding DbContext to connect to SQL Server!!!

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if(environment != "Production")
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<SpiceDbContext>(options =>
                 options.UseSqlServer(connectionString));
}
else
{
    var secretService = new SecretsManagerServices();
    secretService.ConfigureServices(builder.Services);
}


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
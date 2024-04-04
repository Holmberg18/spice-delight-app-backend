using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using spice_delight_app_backend.Data;
using spice_delight_app_backend.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Fetch secrets from AWS Secrets Manager
var secretsManagerService = new SecretsManagerServices();
var dbCredentials = await secretsManagerService.GetSecretAsync("spice-delight-app-backend-db-secret", "us-east-1");
var jwtCredentials = await secretsManagerService.GetSecretAsync("spice-delight-app-jwt-secret", "us-east-1");

var dbConnectionInfo = new DbConnectionInfo
{
    Server = "spice-delight-app-database-1.chegekmyc2zc.us-east-1.rds.amazonaws.com",
    Database = "spice-delight-app-database-1",
    UserId = dbCredentials["username"],
    Password = dbCredentials["password"]
};
var connectionString = $"Server={dbConnectionInfo.Server};" +
                       $"Database={dbConnectionInfo.Database};" +
                       $"User Id={dbConnectionInfo.UserId};" +
                       $"Password={dbConnectionInfo.Password};" +
                       $"Encrypt={(dbConnectionInfo.Encrypt ? "true" : "false")};" +
                       $"TrustServerCertificate={(dbConnectionInfo.TrustServerCertificate ? "true" : "false")};";

builder.Services.AddDbContext<SpiceDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddGraphQLServer().AddQueryType<Query>().AddProjections().AddFiltering().AddSorting();

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtCredentials["Issuer"],
            ValidAudience = jwtCredentials["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtCredentials["Key"]))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL("/graphql");
app.Run();

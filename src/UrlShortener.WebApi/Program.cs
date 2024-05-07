using Microsoft.OpenApi.Models;
using UrlShortener.Persistence.Data.Seeders;
var builder = WebApplication.CreateBuilder(args);

// get configuration
var configuration = builder.Configuration;

// Add Clean-Architecture layers
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => {
    o.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme {
            In = ParameterLocation.Header,
            Description = @"Bearer (paste here your token (remove all brackets) )",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
        });

    o.SwaggerDoc("v1", new OpenApiInfo() {
        Title = "UrlShortener API - v1",
        Version = "v1"
    });
});

// enable CORS to all sources
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() ) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.SeedData();

app.Run();

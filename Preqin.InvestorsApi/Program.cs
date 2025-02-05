using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Preqin.InvestorsApi.Data;
using Preqin.InvestorsApi.DataMappers;
using Preqin.InvestorsApi.DataService;
using Preqin.InvestorsApi.HealthCheck;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:8080", "http://localhost:5140")
            .WithMethods("GET", "POST")
            .AllowAnyHeader());
});
builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IInvestorRepository, InvestorRepository>();
builder.Services.AddScoped<IInvestorMapper, InvestorMapper>();
builder.Services.AddScoped<IInvestorDataService, InvestorDataService>();

// Add health checks
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck<AppDbContext>>("database");

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//https://localhost:<port>/openapi/v1.json to view the generated OpenAPI document.
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");
    //x=> x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1/openapi.json");
}

app.MapHealthChecks("/healthz");

app.MapControllers();

// Docker HEALTHCHECK verification endpoint
app.MapGet("/ping", () => "OK");

app.Run();
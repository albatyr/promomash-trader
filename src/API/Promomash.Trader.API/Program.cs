using Promomash.Trader.API.Configuration.ExceptionHandlers;
using Promomash.Trader.API.Configuration.HostedServices;
using Promomash.Trader.ServiceDefaults;
using Promomash.Trader.UserAccess.Infrastructure;
using Promomash.Trader.UserAccess.Infrastructure.Configuration.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDataSource("traderDb");
builder.AddNpgsqlDbContext<UserAccessContext>(connectionName: "traderDb");

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services
    .AddProblemDetails(options =>
        options.CustomizeProblemDetails = ctx =>
        {
            ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
            ctx.ProblemDetails.Extensions.Add("instance", $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
        });

builder.Services.AddExceptionHandler<ExceptionToProblemDetailsHandler>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<DatabaseInitializationBackgroundService>();

// Register custom modules
builder.Services.AddUserAccessModule();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();
app.MapControllers();
app.MapDefaultEndpoints();

app.Run();

public partial class Program;
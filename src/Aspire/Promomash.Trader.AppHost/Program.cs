using Promomash.Trader.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume();
var traderDb = postgres.AddDatabase("traderDb", databaseName: "traderDb");

var api = builder
    .AddProject<Projects.Promomash_Trader_API>("api")
    .WithExternalHttpEndpoints()
    .WithScalar()
    .WithReference(traderDb)
    .WaitFor(postgres);

builder.AddNpmApp("trader-app", "../../Frontend/Promomash.Trader.App")
    .WithHttpEndpoint(env: "PORT", port: 4300)
    .WithReference(api)
    .WaitFor(api)
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();

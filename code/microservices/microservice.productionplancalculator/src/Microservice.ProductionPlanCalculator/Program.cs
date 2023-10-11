using Microservice.ProductionPlanCalculator.Api;
using Microservice.ProductionPlanCalculator.Infra;
using Platform;
using Platform.Infra.Messaging;

var builder = WebApiApplicationBuilder.Build<Program>(args);

//Dapr for Bus
builder.Services.AddDaprClient();

//Add IoC
builder.Services.AddIoC();

//Health Checks
builder.Services.AddHealthChecks()
    .AddCheck<DaprHealthCheck>("dapr-check");

var app = builder.Build();

app.ConfigureBaseApplicationBuilders();
app.ConfigureBaseEndpointBuilders();

// Configure the Minimal APIs
app.MapDaprSubscription();

// Dapr will send serialized event object vs. being raw CloudEvent
app.UseCloudEvents();
// needed for Dapr pub/sub routing
app.MapSubscribeHandler();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
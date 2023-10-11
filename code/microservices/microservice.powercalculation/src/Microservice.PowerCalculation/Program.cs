using System.Reflection;
using FluentValidation;
using Microservice.PowerCalculation.Api;
using Microservice.PowerCalculation.Api.Validations;
using Microservice.PowerCalculation.Infra;
using Platform;
using Platform.Infra.Messaging;

var builder = WebApiApplicationBuilder.Build<Program>(args);

//MediatR for Commands/Queries
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

//Dapr for Bus
builder.Services.AddDaprClient();

//Add IoC
builder.Services.AddIoC();

//Health Checks
builder.Services.AddHealthChecks()
    .AddCheck<DaprHealthCheck>("dapr-check");

//Health Check UI
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

//MediatR validators
builder.Services.AddValidatorsFromAssemblyContaining<CalculatePowerCommandValidator>();

var app = builder.Build();

app.ConfigureBaseApplicationBuilders();
app.ConfigureBaseEndpointBuilders();

//Health Check UI
app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");

// Configure the Minimal APIs
app.MapProduct();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
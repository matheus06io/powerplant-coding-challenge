using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Exceptions;

namespace Platform;

public static class WebApiApplicationBuilder
{
    public static WebApplicationBuilder Build<TStartup>(string[] args) where TStartup : class
    {
        var builder = WebApplication.CreateBuilder(args);
     
        var serviceName = builder.Configuration["MicroserviceSettings:Service"];

        //Serilog and ElasticSearch
        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.
                WriteTo.Async(writeTo =>
                    writeTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss,fff} [{ThreadId}] {Level:u4} {Message:lj}{NewLine}{Exception}"))
                .Enrich.WithExceptionDetails()
                .Enrich.WithThreadId();
        });
        
        // Configure Open API
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        return builder;
    }


    public static void ConfigureBaseApplicationBuilders(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseSerilogRequestLogging();
    }
    
    public static void ConfigureBaseEndpointBuilders(this IEndpointRouteBuilder app)
    {
        //Health Check Api
        app.MapHealthChecks("/healthz", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        
    }
}

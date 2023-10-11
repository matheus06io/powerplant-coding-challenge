using MediatR;
using Microservice.PowerCalculation.Application.Behaviors;
using Microservice.PowerCalculation.Domain;
using Microservice.PowerCalculation.Domain.Abstractions;
using Platform.Infra.Messaging;
using Platform.Infra.Messaging.Abstractions;

namespace Microservice.PowerCalculation.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddIoC(this IServiceCollection serviceCollection)
    {
        //Add Bus
        serviceCollection.AddScoped<IEventBus, DaprEventBus>();

        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddScoped<IProductionPlanCalculator, ProductionPlanCalculator>();
        
        return serviceCollection;
    }
}
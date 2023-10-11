using Platform.Infra.Messaging;
using Platform.Infra.Messaging.Abstractions;

namespace Microservice.ProductionPlanCalculator.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddIoC(this IServiceCollection serviceCollection)
    {
        //Add Bus
        serviceCollection.AddScoped<IEventBus, DaprEventBus>();

        return serviceCollection;
    }
}
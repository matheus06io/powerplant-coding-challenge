using Dapr;
using Microsoft.AspNetCore.Mvc;
using Platform.Domain.Shared.IntegrationEvents;

namespace Microservice.ProductionPlanCalculator.Api;

internal static class SubscriptionApi
{
    public static RouteGroupBuilder MapDaprSubscription(this IEndpointRouteBuilder routes)
    {
        const string daprPubSubName = "pubsub";
        
        var group = routes.MapGroup("/subscription");
        group.WithTags("Subscription");

        // Dapr subscription in /dapr/subscribe sets up this route
        group.MapPost($"/{nameof(CalculateProductionPlanAsyncIntegrationEvent)}", [Topic(daprPubSubName, nameof(CalculateProductionPlanAsyncIntegrationEvent))] 
            async Task<IResult> (CalculateProductionPlanAsyncIntegrationEvent @event) => {
            Console.WriteLine("CalculateProductionPlanAsyncIntegrationEvent received => " +
                              $"EventId: {@event.Id} " +
                              $"CreationDate: {@event.CreationDate} " +
                              $"RequestId: {@event.RequestId} " +
                              $"Payload: {@event.Payload}");
            
            return Results.Ok(@event);
        }).ExcludeFromDescription();
            
        return group;
    }
    
}
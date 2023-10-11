using Platform.Infra.Messaging.Abstractions;

namespace Platform.Domain.Shared.IntegrationEvents;


public record CalculateProductionPlanAsyncIntegrationEvent : IntegrationEvent
{
    public Guid RequestId { get; private set; }
    public string Payload { get; private set; }
    
    public CalculateProductionPlanAsyncIntegrationEvent(Guid requestId, string payload)
    {
        RequestId = requestId;
        Payload = payload;
    }
}
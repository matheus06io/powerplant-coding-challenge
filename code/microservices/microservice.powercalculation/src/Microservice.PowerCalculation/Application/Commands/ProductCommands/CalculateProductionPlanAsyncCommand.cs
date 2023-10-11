using System.Text.Json;
using FluentResults;
using MediatR;
using Microservice.PowerCalculation.Application.Dto;
using Platform.Domain.Shared.IntegrationEvents;
using Platform.ErrorHandling.ApplicationErrors;
using Platform.Infra.Messaging.Abstractions;

namespace Microservice.PowerCalculation.Application.Commands.ProductCommands;

public class CalculateProductionPlanAsyncCommand : IRequest<FluentResults.Result<ProductionPlanAsyncResponse>>
{
    public ProductionPlanRequest Request { get;  private set; }
   
    public CalculateProductionPlanAsyncCommand(ProductionPlanRequest request)
    {
        Request = request;
    }
}

public class CalculatePowerAsyncCommandHandler : IRequestHandler<CalculateProductionPlanAsyncCommand, FluentResults.Result<ProductionPlanAsyncResponse>>
{
   
    private ILogger<CalculatePowerAsyncCommandHandler> _logger;
    private IEventBus _eventBus;
    
    public CalculatePowerAsyncCommandHandler(ILogger<CalculatePowerAsyncCommandHandler> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }
    
    public async Task<FluentResults.Result<ProductionPlanAsyncResponse>> Handle(CalculateProductionPlanAsyncCommand asyncCommand, CancellationToken cancellationToken)
    {
        try
        {
            //TODO Save to DB with created status and return the Id to be fetched by the user
            var requestId = Guid.NewGuid();
                
            await _eventBus.Publish(new CalculateProductionPlanAsyncIntegrationEvent(requestId,  JsonSerializer.Serialize(asyncCommand.Request)));
            _logger.LogInformation("Integration Event Sent: CalculateProductionPlanAsyncIntegrationEvent");
            
            return Result.Ok(Mapper.MapToProductionPlanAsyncResponse(requestId));
        }
        catch (Exception e)
        {
            return Result.Fail(new CalculationError());
        }
    }
}
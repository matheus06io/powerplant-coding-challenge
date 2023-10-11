using Microservice.PowerCalculation.Application.Commands.ProductCommands;
using Microservice.PowerCalculation.Domain;

namespace Microservice.PowerCalculation.Application.Dto;

public static class Mapper
{
    public static IEnumerable<ProductionPlanResponse> MapToProductionPlanResponse(List<ProductionPlan> productionPlan)
    {
        return productionPlan.Select(c => new ProductionPlanResponse
        {
            Name = c.Name,
            Power = c.Power
        });
    }
    
    public static ProductionPlanAsyncResponse MapToProductionPlanAsyncResponse(Guid requestId)
    {
        return new ProductionPlanAsyncResponse { Id = requestId };
    }
    
    public static CalculateProductionPlanCommand MapToProductionPlanCommand (ProductionPlanRequest productionPlanRequest)
    {
        return new CalculateProductionPlanCommand(productionPlanRequest);
    }
    
    public static CalculateProductionPlanAsyncCommand MapToProductionPlanAsyncCommand (ProductionPlanRequest productionPlanRequest)
    {
        return new CalculateProductionPlanAsyncCommand(productionPlanRequest);
    }
}
using FluentResults;
using MediatR;
using Microservice.PowerCalculation.Application.Dto;
using Microservice.PowerCalculation.Domain.Abstractions;
using Platform.ErrorHandling.ApplicationErrors;

namespace Microservice.PowerCalculation.Application.Commands.ProductCommands;

public class CalculateProductionPlanCommand : IRequest<FluentResults.Result<IEnumerable<ProductionPlanResponse>>>
{
    public ProductionPlanRequest Request { get;  private set; }
   
    public CalculateProductionPlanCommand(ProductionPlanRequest request)
    {
        Request = request;
    }
}

public class CalculatePowerCommandHandler : IRequestHandler<CalculateProductionPlanCommand, FluentResults.Result<IEnumerable<ProductionPlanResponse>>>
{
    private readonly IProductionPlanCalculator _productionPlanCalculator;
    
    public CalculatePowerCommandHandler(IProductionPlanCalculator productionPlanCalculator)
    {
        _productionPlanCalculator = productionPlanCalculator;
    }
    
    public async Task<FluentResults.Result<IEnumerable<ProductionPlanResponse>>> Handle(CalculateProductionPlanCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var productionPlanList =  _productionPlanCalculator.CalculateProductionPlan(command.Request);
            return Result.Ok(Mapper.MapToProductionPlanResponse(productionPlanList));
        }
        catch (Exception e)
        {
            return Result.Fail(new CalculationError());
        }
    }
}
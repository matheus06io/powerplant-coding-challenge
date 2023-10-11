using MediatR;
using Microservice.PowerCalculation.Application.Dto;
using Platform.ErrorHandling;

namespace Microservice.PowerCalculation.Api;

internal static class ProductionPlanApi
{
    public static RouteGroupBuilder MapProduct(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/productionplan");
        group.WithTags("PowerPlant");

        group.MapPost("/", async Task<IResult> (ProductionPlanRequest productionPlanRequest, IMediator mediator) =>
        {
            var result = await mediator.Send(Mapper.MapToProductionPlanCommand(productionPlanRequest));
            return result.IsFailed ? ProblemHelper.Problem(result.Errors) : TypedResults.Ok(result.Value);
        });
        
        group.MapPut("/asyncCalculation", async Task<IResult> (ProductionPlanRequest productionPlanRequest, IMediator mediator) =>
        {
            var result = await mediator.Send(Mapper.MapToProductionPlanAsyncCommand(productionPlanRequest));
            return result.IsFailed ? ProblemHelper.Problem(result.Errors) : TypedResults.Accepted($"/asyncCalculationReturn/{result.Value.Id}", result.Value);
        });
        
        group.MapGet("/asyncCalculationReturn/{requestId:guid}", Task<IResult> (Guid requestId, IMediator mediator) => Task.FromResult<IResult>(TypedResults.Ok(requestId)));
        
        return group;
    }
}
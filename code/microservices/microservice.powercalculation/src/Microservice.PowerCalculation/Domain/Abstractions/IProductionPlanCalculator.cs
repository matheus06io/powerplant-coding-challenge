using Microservice.PowerCalculation.Application.Dto;

namespace Microservice.PowerCalculation.Domain.Abstractions;

public interface IProductionPlanCalculator
{
    List<ProductionPlan> CalculateProductionPlan(ProductionPlanRequest loadData);
}
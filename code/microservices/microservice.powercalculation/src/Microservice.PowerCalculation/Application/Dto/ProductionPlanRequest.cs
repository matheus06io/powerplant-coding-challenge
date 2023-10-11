namespace Microservice.PowerCalculation.Application.Dto;

public class ProductionPlanRequest
{
    public double Load { get; set; }
    public FuelsRequest Fuels { get; set; }
    public List<PowerPlantRequest> PowerPlants { get; set; }
}
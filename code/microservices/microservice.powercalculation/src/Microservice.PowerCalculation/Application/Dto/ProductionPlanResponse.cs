using System.Text.Json.Serialization;

namespace Microservice.PowerCalculation.Application.Dto;

public class ProductionPlanResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("p")]
    public double Power { get; set; }
}
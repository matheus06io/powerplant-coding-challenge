namespace Microservice.PowerCalculation.Application.Dto;

public class PowerPlantRequest
{
    public string Name { get; set; }
    public PowerPlantType Type { get; set; }
    public double Efficiency { get; set; }
    public int Pmin { get; set; }
    public int Pmax { get; set; }
}
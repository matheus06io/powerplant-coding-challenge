using System.Text.Json.Serialization;

namespace Microservice.PowerCalculation.Application.Dto;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PowerPlantType
{
    GasFired,
    Turbojet,
    WindTurbine
}
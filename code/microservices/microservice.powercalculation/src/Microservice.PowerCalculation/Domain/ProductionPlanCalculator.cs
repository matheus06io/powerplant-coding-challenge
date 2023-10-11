using Microservice.PowerCalculation.Application.Dto;
using Microservice.PowerCalculation.Domain.Abstractions;

namespace Microservice.PowerCalculation.Domain;

public class ProductionPlanCalculator : IProductionPlanCalculator
{
    public List<ProductionPlan> CalculateProductionPlan(ProductionPlanRequest loadData)
    {
        List<ProductionPlan> productionPlan = new List<ProductionPlan>();

        // Calculate wind power production
        double windPowerProduction = loadData.Load * loadData.Fuels.Wind / 100.0;
        foreach (PowerPlantRequest powerPlant in loadData.PowerPlants)
        {
            if (powerPlant.Type == PowerPlantType.WindTurbine)
            {
                double windPower = Math.Min(windPowerProduction, powerPlant.Pmax);
                productionPlan.Add(new ProductionPlan(powerPlant.Name, windPower));
                windPowerProduction -= windPower;
            }
        }

        // Calculate gas-fired and turbojet power production
        foreach (PowerPlantRequest powerPlant in loadData.PowerPlants)
        {
            if (powerPlant.Type == PowerPlantType.GasFired || powerPlant.Type == PowerPlantType.Turbojet)
            {
                double efficiency = powerPlant.Efficiency;
                double pMin = powerPlant.Pmin;
                double pMax = powerPlant.Pmax;
                double fuelCost = powerPlant.Type == PowerPlantType.GasFired
                    ? loadData.Fuels.Gas
                    : loadData.Fuels.Kerosine;

                double power = 0.0;

                if (loadData.Load > 0)
                {
                    double maxAvailablePower = (pMax - pMin) * efficiency;
                    double marginalCost = fuelCost / efficiency;

                    if (marginalCost < loadData.Fuels.Wind)
                    {
                        power = Math.Min(loadData.Load, maxAvailablePower);
                    }
                }

                productionPlan.Add(new ProductionPlan( powerPlant.Name, power));
            }
        }

        return productionPlan;
    }
}
using FluentValidation;
using Microservice.PowerCalculation.Application.Commands.ProductCommands;
using Microservice.PowerCalculation.Application.Dto;

namespace Microservice.PowerCalculation.Api.Validations
{
    public  class CalculatePowerCommandValidator : AbstractValidator<CalculateProductionPlanCommand>
    {
        public  CalculatePowerCommandValidator()
        {
            RuleFor(calculatePowerCommand => calculatePowerCommand.Request.Load).GreaterThanOrEqualTo(0);
            
            RuleFor(createProductCommand => createProductCommand.Request.Fuels.Co2).GreaterThanOrEqualTo(0);
            RuleFor(createProductCommand => createProductCommand.Request.Fuels.Gas).GreaterThanOrEqualTo(0);
            RuleFor(createProductCommand => createProductCommand.Request.Fuels.Kerosine).GreaterThanOrEqualTo(0);
            RuleFor(createProductCommand => createProductCommand.Request.Fuels.Wind).GreaterThanOrEqualTo(0);

            RuleForEach(createProductCommand => createProductCommand.Request.PowerPlants)
                .SetValidator(new PowerPlantValidator());
        }
    }
    
    public class PowerPlantValidator : AbstractValidator<PowerPlantRequest> 
    {
        public PowerPlantValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Efficiency).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Pmin).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Pmax).GreaterThanOrEqualTo(0);
        }
    }
}
using Platform.Domain.Abstractions;

namespace Microservice.PowerCalculation.Domain;

public class ProductionPlan : AggregateRoot<Guid>
{
    public string Name { get; set; }
    public double Power { get; set; }
    
    protected ProductionPlan( ){ }

    public ProductionPlan(string name, double power) 
        : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(nameof(name));

        Name = name;
        Power = power;
    }
}
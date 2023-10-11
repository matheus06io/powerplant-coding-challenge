namespace Microservice.PowerCalculation.Test.Domain;

public class ProductionPlanCalculatorTest
{
    private readonly IFixture _fixture;
    private readonly IProductionPlanCalculator _productionPlanCalculator;
    
    public ProductionPlanCalculatorTest()
    {
        _fixture = new Fixture();
        _productionPlanCalculator = new ProductionPlanCalculator();
    }
    
    [Fact]
    public void ProductionPlanCalculator_WhenCalculateProductionPlan_ShouldReturnTypeEqualsToListOfProductionPlan()
    {
        //Arrange
        var productionPlanRequest = _fixture.Build<ProductionPlanRequest>().Create();

        //Action
        var productionPlansList = _productionPlanCalculator.CalculateProductionPlan(productionPlanRequest);
        
        //Assert
        Assert.IsType<List<ProductionPlan>>(productionPlansList);
    }
}
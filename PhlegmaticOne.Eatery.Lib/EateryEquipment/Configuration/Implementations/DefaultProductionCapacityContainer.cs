namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public class DefaultProductionCapacityContainer : IProductionCapacityContainer
{
    private readonly IDictionary<Type, int> _processesMaximalIngredientsToPrepare;
    private readonly IDictionary<Type, int> _currentProcesses;

    public DefaultProductionCapacityContainer(IDictionary<Type, int> processesMaximalIngredientsToPrepare)
    {
        _processesMaximalIngredientsToPrepare = processesMaximalIngredientsToPrepare;
        _currentProcesses = new Dictionary<Type, int>(processesMaximalIngredientsToPrepare);
    }
    public static IProductionCapacityContainerBuilder GetDefaultProductionCapacityContainerBuilder() =>
        new DefaultProductionCapacityContainerBuilder();

    public void DecreaseCapacity(Type processType)
    {
        if(_currentProcesses.TryGetValue(processType, out int currentValue))
        {
            --currentValue;
            _currentProcesses.Remove(processType);
            _currentProcesses.Add(processType, currentValue);
        }
    }

    public int GetCurrentCapacityOfProcess(Type processType)
    {
        if (_currentProcesses.TryGetValue(processType, out int currentValue))
        {
            return currentValue;
        }
        return int.MinValue;
    }

    public int GetMaximalProductsToProcess(Type processType)
    {
        if(_processesMaximalIngredientsToPrepare.TryGetValue(processType, out int maxValue))
        {
            return maxValue;
        }
        return int.MinValue;
    }

    public void IncreaseCapacity(Type processType)
    {
        if (_currentProcesses.TryGetValue(processType, out int currentValue))
        {
            ++currentValue;
            _currentProcesses.Remove(processType);
            _currentProcesses.Add(processType, currentValue);
        }
    }
}

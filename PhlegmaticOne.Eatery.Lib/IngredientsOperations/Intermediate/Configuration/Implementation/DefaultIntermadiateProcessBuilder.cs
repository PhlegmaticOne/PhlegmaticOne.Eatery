using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represent default intermadiate process builder
/// </summary>
/// <typeparam name="TProcess"></typeparam>
public class DefaultIntermadiateProcessBuilder<TProcess> : IIntermediateProcessBuilder<TProcess>
                                                           where TProcess : IntermediateProcess, new()
{
    private readonly List<TProcess> _processesList = new();
    private readonly List<Type> _ingredientType = new();
    private Money? _money;
    public IIntermediateProcessBuilder<TProcess> DishMayContain<TIngredient>()
    {
        _ingredientType.Add(typeof(TIngredient));
        return this;
    }
    public IIntermediateProcessBuilder<TProcess> WithCost(Money money)
    {
        _money = Money.ConvertToUSD(money);
        return this;
    }
    public void WithTimeToFinish(TimeSpan timeToFinish)
    {
        _processesList.Add(new TProcess()
        {
            PreferableTypesToProcess = _ingredientType.ToList(),
            Price = _money,
            TimeToFinish = timeToFinish
        });
    }
    public void SetDefaultProcess(Money money, TimeSpan timeSpan)
    {
        _processesList.Add(new TProcess()
        {
            PreferableTypesToProcess = null,
            Price = money,
            TimeToFinish = timeSpan
        });

    }
    public IList<TProcess> Build() => _processesList;
    public override string ToString() => GetType().Name;
}

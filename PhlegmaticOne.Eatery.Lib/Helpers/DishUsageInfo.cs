namespace PhlegmaticOne.Eatery.Lib.Helpers;

public class DishUsageInfo
{
    public DishUsageInfo(IReadOnlyDictionary<Type, double> allUsedIngredients, Money earnedMoney, Type dishType)
    {
        AllUsedIngredients = allUsedIngredients;
        EarnedMoney = earnedMoney;
        DishType = dishType;
    }
    public IReadOnlyDictionary<Type, double> AllUsedIngredients { get; }
    public Money EarnedMoney { get; }
    public Type DishType { get; }
}
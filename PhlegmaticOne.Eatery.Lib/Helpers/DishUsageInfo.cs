namespace PhlegmaticOne.Eatery.Lib.Helpers;
/// <summary>
/// Represents container for dish usage info
/// </summary>
public class DishUsageInfo
{
    /// <summary>
    /// Initializes new DishUsageInfo instance
    /// </summary>
    /// <param name="allUsedIngredients">All used ingredients for dish</param>
    /// <param name="earnedMoney">Earned money on this dish</param>
    /// <param name="dishType">Dish tyoe</param>
    public DishUsageInfo(IReadOnlyDictionary<Type, double> allUsedIngredients, Money earnedMoney, Type dishType)
    {
        AllUsedIngredients = allUsedIngredients;
        EarnedMoney = earnedMoney;
        DishType = dishType;
    }
    /// <summary>
    /// All used ingredients
    /// </summary>
    public IReadOnlyDictionary<Type, double> AllUsedIngredients { get; }
    /// <summary>
    /// Earned money
    /// </summary>
    public Money EarnedMoney { get; }
    /// <summary>
    /// Dish type
    /// </summary>
    public Type DishType { get; }
    public override string ToString() => string.Format("{0}. Earned: {1}", DishType.Name, EarnedMoney);
}
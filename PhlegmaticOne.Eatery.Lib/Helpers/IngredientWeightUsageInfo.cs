namespace PhlegmaticOne.Eatery.Lib.Helpers;
/// <summary>
/// Represents container for ingredient usage info
/// </summary>
public class IngredientWeightUsageInfo
{
    /// <summary>
    /// Initializes new IngredientUsageInfo
    /// </summary>
    /// <param name="ingredientType">Specified ingredient type</param>
    /// <param name="usedWeight">Used ingredient weight</param>
    public IngredientWeightUsageInfo(Type ingredientType, double usedWeight)
    {
        IngredientType = ingredientType;
        UsedWeight = usedWeight;
    }
    /// <summary>
    /// Ingredient type
    /// </summary>
    public Type IngredientType { get; }
    /// <summary>
    /// Used weight
    /// </summary>
    public double UsedWeight { get; }
    public override string ToString() => string.Format("{0}. Used weight: {1}", IngredientType.Name, UsedWeight);
}

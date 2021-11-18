namespace PhlegmaticOne.Eatery.Lib.Helpers;

public class IngredientTimesUsageInfo
{
    /// <summary>
    /// Initializes new IngredientTimesUsageInfo instance
    /// </summary>
    /// <param name="ingredientType">Specified ingredient type</param>
    /// <param name="timesUsed">Specified times used</param>
    public IngredientTimesUsageInfo(Type ingredientType, int timesUsed)
    {
        IngredientType = ingredientType;
        TimesUsed = timesUsed;
    }
    /// <summary>
    /// Ingredient type
    /// </summary>
    public Type IngredientType { get; }
    /// <summary>
    /// Times ingredient was used
    /// </summary>
    public int TimesUsed { get; }
    public override string ToString() => string.Format("{0}. Used times: {1}", IngredientType.Name, TimesUsed);
}

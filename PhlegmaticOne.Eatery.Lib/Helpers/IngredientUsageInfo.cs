namespace PhlegmaticOne.Eatery.Lib.Helpers;

public class IngredientUsageInfo
{
    public IngredientUsageInfo(Type ingredientType, double usedWeight)
    {
        IngredientType = ingredientType;
        UsedWeight = usedWeight;
    }

    public Type IngredientType { get; }
    public double UsedWeight { get; }
}

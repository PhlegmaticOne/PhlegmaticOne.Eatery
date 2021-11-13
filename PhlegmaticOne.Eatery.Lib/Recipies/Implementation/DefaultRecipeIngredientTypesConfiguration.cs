using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public class DefaultRecipeIngredientTypesConfiguration : IRecipeIngredientTypesConfiguration
{
    private Type _configuringIngredientType;
    private readonly Dictionary<Type, double>? _recipeTypes = new();
    //public IRecipeIngredientTypesConfiguration FromStorage<TStorage>(Func<TStorage, bool> predicate) where TStorage : Storage, new()
    //{
    //    var fittedStorages = _storageContainer.OfStorageType<TStorage>();
    //    if(fittedStorages.Any() == false)
    //    {
    //        throw new ArgumentException("Storages are not registered in container");
    //    }
    //    _fittedStorages = fittedStorages;
    //    return this;
    //}
    public IRecipeIngredientTypesConfiguration NeedIngredient<TIngredient>() where TIngredient : Ingredient, new()
    {
        _configuringIngredientType = typeof(TIngredient);
        return this;
    }
    public void InWeightOf(double weight)
    {
        if (weight <= 0)
        {
            throw new ArgumentException("You can't ask value less or equal to 0");
        }
        _recipeTypes.Add(_configuringIngredientType, weight);
    }
    public IDictionary<Type, double> Configure() => _recipeTypes;
}
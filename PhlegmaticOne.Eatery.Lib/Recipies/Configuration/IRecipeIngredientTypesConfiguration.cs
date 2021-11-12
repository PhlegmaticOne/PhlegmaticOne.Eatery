using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public interface IRecipeIngredientTypesConfiguration
{
    //IRecipeIngredientTypesConfiguration FromStorage<TStorage>(Func<TStorage, bool> predicate) where TStorage : Storage, new();
    IRecipeIngredientTypesConfiguration NeedIngredient<TIngredient>() where TIngredient : Ingredient, new();
    void InWeightOf(double weight);
    //void SetSource(IStorageContainer storageContainer);
    IDictionary<Type, double> Configure();
}

using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public class RecipeController
{
    private readonly Recipe _recipe;
    private readonly IStorageContainer _storageContainer;

    public RecipeController(Recipe recipe, IStorageContainer storageContainer)
    {
        _recipe = recipe ?? throw new ArgumentNullException(nameof(recipe));
        _storageContainer = storageContainer ?? throw new ArgumentNullException(nameof(storageContainer));
    }
    public Dish Prepare()
    {
        var dish = new Dish(new Money(0, "USD"), 0, _recipe.Name);
        var neededIngredientsInfo = _recipe.GetIngredientsTakesPartInPreparing();
        var processesToPrepare = _recipe.GetProcessesQueueToPrepareDish();
        var allStorages = _storageContainer.AllStorages();
        Ingredient necessaryIngredient = default;
        while (processesToPrepare.Any())
        {
            var process = processesToPrepare.Dequeue();
            if (process is IngredientProcess ingredientProcess)
            {
                if(ingredientProcess is AddingProcess addingProcess)
                {
                    necessaryIngredient = GetIngredient(addingProcess, allStorages, neededIngredientsInfo);
                }
                if(necessaryIngredient is null)
                {
                    throw new InvalidOperationException($"There are not enough of {ingredientProcess.CurrentIngredientType?.Name} in all storages");
                }
                ingredientProcess.Update(dish, necessaryIngredient);
            }
            else if (process is IntermediateProcess intermediateProcess)
            {
                intermediateProcess.Update(dish);
            }
        }
        return dish;
    }
    private Ingredient GetIngredient(AddingProcess addingProcess, IEnumerable<Storage> storages,
                                     IReadOnlyDictionary<Type, double> neededIngredientsInfo)
    {
        var processingIngredientType = addingProcess.CurrentIngredientType;
        neededIngredientsInfo.TryGetValue(processingIngredientType, out double necessaryWeight);
        foreach (var storage in storages)
        {
            var ingredient = storage.TryRetrieve(processingIngredientType, necessaryWeight);
            if (ingredient is not null)
            {
                return ingredient;
            }
        }
        return null;
    }
}

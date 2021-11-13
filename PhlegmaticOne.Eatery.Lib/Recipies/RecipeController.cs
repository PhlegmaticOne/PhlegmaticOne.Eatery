using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public class RecipeController
{
    private readonly Recipe _recipe;
    private readonly IStorageContainer _storageContainer;
    private readonly IProductionCapacityContainer _productionCapacityContainer;

    public RecipeController(Recipe recipe, IStorageContainer storageContainer, IProductionCapacityContainer productionCapacityContainer)
    {
        _recipe = recipe ?? throw new ArgumentNullException(nameof(recipe));
        _storageContainer = storageContainer ?? throw new ArgumentNullException(nameof(storageContainer));
        _productionCapacityContainer = productionCapacityContainer;
    }
    public PrepareResult Prepare()
    {
        var dish = new Dish(new Money(0, "USD"), 0, _recipe.Name);
        var neededIngredientsInfo = _recipe.GetIngredientsTakesPartInPreparing();
        var processesToPrepare = _recipe.GetProcessesQueueToPrepareDish();
        var allStorages = _storageContainer.AllStorages();
        Ingredient necessaryIngredient = default;
        while (processesToPrepare.Any())
        {
            var process = processesToPrepare.Dequeue();
            _productionCapacityContainer.DecreaseCapacity(process.GetType());
            if (_productionCapacityContainer.GetCurrentCapacityOfProcess(process.GetType()) == 0)
            {
                return new PrepareResult(null, PrepareResultType.NotEnoughProductionCapacity);
            }
            if (process is IngredientProcess ingredientProcess)
            {
                if(ingredientProcess is AddingProcess addingProcess)
                {
                    necessaryIngredient = GetIngredient(addingProcess, allStorages, neededIngredientsInfo);
                }
                if(necessaryIngredient is null)
                {
                    return new PrepareResult(null, PrepareResultType.NotEnoughIngredients);
                }
                ingredientProcess.Update(dish, necessaryIngredient);
            }
            else if (process is IntermediateProcess intermediateProcess)
            {
                intermediateProcess.Update(dish);
            }
        }
        return new PrepareResult(dish, PrepareResultType.Success);
    }
    private static Ingredient GetIngredient(AddingProcess addingProcess, IEnumerable<Storage> storages,
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

public class PrepareResult
{
    public PrepareResult(Dish dish, PrepareResultType prepareResultType)
    {
        Dish = dish;
        PrepareResultType = prepareResultType;
    }
    public Dish Dish { get; }
    public PrepareResultType PrepareResultType { get; }
}
public enum PrepareResultType
{
    Success,
    NotEnoughIngredients,
    NotEnoughProductionCapacity,
}

using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Storages;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public class Recipe
{
    public Recipe() => (IngredientsTakesPartInPreparing, ProcessesQueueToPrepareDish) =
                          (new Dictionary<Type, double>(), new());
    public Recipe(string name,
                  IDictionary<Type, double> ingredientsTakesPartInPreparing,
                  Queue<DomainProductProcess> processesQueueToPrepareDish)
    {
        Name = name;
        IngredientsTakesPartInPreparing = ingredientsTakesPartInPreparing ?? throw new ArgumentNullException(nameof(ingredientsTakesPartInPreparing));
        ProcessesQueueToPrepareDish = processesQueueToPrepareDish ?? throw new ArgumentNullException(nameof(processesQueueToPrepareDish));
    }

    public string Name { get; internal set; }
    internal IDictionary<Type, double> IngredientsTakesPartInPreparing { get; set; }
    internal Queue<DomainProductProcess> ProcessesQueueToPrepareDish { get; set; }
    public IReadOnlyDictionary<Type, double> GetIngredientsTakesPartInPreparing() =>
        new ReadOnlyDictionary<Type, double>(IngredientsTakesPartInPreparing);
    public Queue<DomainProductProcess> GetProcessesQueueToPrepareDish() => new(ProcessesQueueToPrepareDish);
    public static IRecipeBuilder GetDefaultRecipeBuilder(string recipeName) => new DefaultRecipeBuilder(recipeName);
    public override string ToString() => string.Format("{0}. Ingredients: {1}",
                                         GetType().Name, string.Join(',', IngredientsTakesPartInPreparing));
    public override bool Equals(object? obj) => obj is Recipe dishRecipe &&
                                                IngredientsTakesPartInPreparing.Except(dishRecipe.IngredientsTakesPartInPreparing)
                                                .Any() == false &&
                                                ProcessesQueueToPrepareDish.Except(dishRecipe.ProcessesQueueToPrepareDish)
                                                .Any() == false;
    public override int GetHashCode() => IngredientsTakesPartInPreparing.GetHashCode() ^ ProcessesQueueToPrepareDish.GetHashCode();
}

using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public class Recipe
{
    internal Recipe() => (IngredientsTakesPartInPreparing, ProcessesQueueToPrepareDish) =
                          (new Dictionary<Type, double>(), new());
    [Newtonsoft.Json.JsonConstructor]
    internal Recipe(string name,
                    Dictionary<Type, double> ingredientsTakesPartInPreparing,
                    Queue<DomainProductProcess> processesQueueToPrepareDish)
    {
        Name = name;
        IngredientsTakesPartInPreparing = ingredientsTakesPartInPreparing as Dictionary<Type, double> ?? throw new ArgumentNullException(nameof(ingredientsTakesPartInPreparing));
        ProcessesQueueToPrepareDish = processesQueueToPrepareDish ?? throw new ArgumentNullException(nameof(processesQueueToPrepareDish));
    }
    [JsonProperty]
    public string Name { get; set; }
    [JsonProperty]
    internal Dictionary<Type, double> IngredientsTakesPartInPreparing { get; set; }
    [JsonProperty]
    internal Queue<DomainProductProcess> ProcessesQueueToPrepareDish { get; set; }
    [Newtonsoft.Json.JsonIgnore]
    internal Type DishType { get; set; }
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

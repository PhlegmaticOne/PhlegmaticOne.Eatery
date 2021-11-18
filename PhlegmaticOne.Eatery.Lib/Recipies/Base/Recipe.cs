using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Recipies;
/// <summary>
/// Represents instance for recipe
/// </summary>
public class Recipe : IEquatable<Recipe>
{
    /// <summary>
    /// Initializes new Recipe instance
    /// </summary>
    internal Recipe() => (IngredientsTakesPartInPreparing, ProcessesQueueToPrepareDish) =
                          (new Dictionary<Type, double>(), new());
    /// <summary>
    /// Initializes new Recipe instance
    /// </summary>
    /// <param name="name">Specified recipe name</param>
    /// <param name="ingredientsTakesPartInPreparing">Specified ingredients takes part in preparing</param>
    /// <param name="processesQueueToPrepareDish">Specified processes queue to prepare dish</param>
    /// <exception cref="ArgumentNullException">Ingredients or processes is null</exception>
    [Newtonsoft.Json.JsonConstructor]
    internal Recipe(string name,
                    Dictionary<Type, double> ingredientsTakesPartInPreparing,
                    Queue<DomainProductProcess> processesQueueToPrepareDish)
    {
        Name = name;
        IngredientsTakesPartInPreparing = ingredientsTakesPartInPreparing ?? throw new ArgumentNullException(nameof(ingredientsTakesPartInPreparing));
        ProcessesQueueToPrepareDish = processesQueueToPrepareDish ?? throw new ArgumentNullException(nameof(processesQueueToPrepareDish));
    }
    /// <summary>
    /// Recipe name
    /// </summary>
    [JsonProperty]
    public string Name { get; set; }
    /// <summary>
    /// Ingredients takes part in preparing
    /// </summary>
    [JsonProperty]
    internal Dictionary<Type, double> IngredientsTakesPartInPreparing { get; set; }
    /// <summary>
    /// Processes queue to prepare dish
    /// </summary>
    [JsonProperty]
    internal Queue<DomainProductProcess> ProcessesQueueToPrepareDish { get; set; }
    /// <summary>
    /// Preparing dich type
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    internal Type DishType { get; set; }
    /// <summary>
    /// Gets all ingredients that are needed to prepare a dish by current recipe
    /// </summary>
    public IReadOnlyDictionary<Type, double> GetIngredientsTakesPartInPreparing() =>
        new ReadOnlyDictionary<Type, double>(IngredientsTakesPartInPreparing);
    /// <summary>
    /// Gets all processes that are needed to prepare a dish by current recipe
    /// </summary>
    public Queue<DomainProductProcess> GetProcessesQueueToPrepareDish() => new(ProcessesQueueToPrepareDish);
    /// <summary>
    /// Gets default builder for recipe
    /// </summary>
    /// <param name="recipeName">Specified recipe name</param>
    internal static IRecipeBuilder GetDefaultRecipeBuilder(string recipeName) => new DefaultRecipeBuilder(recipeName);
    public override string ToString() => Name;
    public override bool Equals(object? obj) => Equals(obj as Recipe);
    public override int GetHashCode() => IngredientsTakesPartInPreparing.GetHashCode() ^ ProcessesQueueToPrepareDish.GetHashCode();
    public bool Equals(Recipe? other) => other is not null &&
                                          IngredientsTakesPartInPreparing.Except(other.IngredientsTakesPartInPreparing)
                                          .Any() == false &&
                                          ProcessesQueueToPrepareDish.Except(other.ProcessesQueueToPrepareDish)
                                          .Any() == false;
}

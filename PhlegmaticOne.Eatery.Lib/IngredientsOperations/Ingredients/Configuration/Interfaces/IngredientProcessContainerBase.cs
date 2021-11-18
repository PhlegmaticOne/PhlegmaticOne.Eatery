using PhlegmaticOne.Eatery.Lib.Extensions;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents base container for othes ingredient processes containers
/// </summary>
public abstract class IngredientProcessContainerBase
{
    /// <summary>
    /// Initializes new IngredientProcessContainerBase instance
    /// </summary>
    public IngredientProcessContainerBase() { }
    /// <summary>
    /// Possible ingredient types, instances of which can be operated by specified process
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    internal readonly Dictionary<Type, List<IngredientProcess>> PossibleTypesToProcess;
    /// <summary>
    /// Initializes new default process container
    /// </summary>
    /// <param name="possibleTypesToProcess">Possible ingredient types, instances of which can be operated by specified process</param>
    /// <exception cref="ArgumentNullException">PossibleTypesToProcess is null</exception>
    [Newtonsoft.Json.JsonConstructor]
    protected IngredientProcessContainerBase(Dictionary<Type, List<IngredientProcess>> possibleTypesToProcess) =>
        PossibleTypesToProcess = possibleTypesToProcess ?? throw new ArgumentNullException(nameof(possibleTypesToProcess));
    /// <summary>
    /// Tries to update process for ingredient type
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    /// <typeparam name="TProcess">Ingredient process type</typeparam>
    /// <param name="process">New process to set</param>
    internal virtual bool TryUpdate<TProcess, TIngredient>(TProcess process)
                         where TProcess : IngredientProcess, new()
                         where TIngredient : Ingredient, new()
    {
        if (process is null) return false;
        if (PossibleTypesToProcess.TryGetValue(typeof(TProcess), out var ingredientProcesses))
        {
            var fitted = ingredientProcesses.FirstOrDefault(p => p.CurrentIngredientType == typeof(TIngredient));
            if (fitted is not null)
            {
                ingredientProcesses.Remove(fitted);
                ingredientProcesses.Add(process);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Gets ingredient process of specified type related to specified ingredient
    /// </summary>
    internal virtual TProcess GetProcess<TProcess, TIngredient>()
                              where TProcess : IngredientProcess, new()
                              where TIngredient : Ingredient, new()
    {
        if (PossibleTypesToProcess.TryGetValue(typeof(TProcess), out var processes))
        {
            return processes.FirstOrDefault(p => p.CurrentIngredientType == typeof(TIngredient)) as TProcess;
        }
        return null;
    }
    /// <summary>
    /// Gets min price ingredient process from all processes in container
    /// </summary>
    public IngredientProcess MinPriceProcess()
    {
        IngredientProcess result = null;
        foreach (var process in PossibleTypesToProcess.Values)
        {
            var temp = process.MaxBy(x => x.Price.Amount);
            if (result is null || result.Price.Amount < temp.Price.Amount)
            {
                result = temp;
            }
        }
        return null;
    }
    /// <summary>
    /// Gets max price ingredient process from all processes in container
    /// </summary>
    public IngredientProcess MaxPriceProcess()
    {
        IngredientProcess result = null;
        foreach (var process in PossibleTypesToProcess.Values)
        {
            var temp = process.MinBy(x => x.Price.Amount);
            if (result is null || result.Price.Amount > temp.Price.Amount)
            {
                result = temp;
            }
        }
        return null;
    }
    /// <summary>
    /// Gets min time ingredient process from all processes in container
    /// </summary>
    public IngredientProcess MinTimeProcess()
    {
        IngredientProcess result = null;
        foreach (var process in PossibleTypesToProcess.Values)
        {
            var temp = process.MinBy(x => x.TimeToFinish);
            if (result is null || result.TimeToFinish > temp.TimeToFinish)
            {
                result = temp;
            }
        }
        return null;
    }
    /// <summary>
    /// Gets max time ingredient process from all processes in container
    /// </summary>
    public IngredientProcess MaxTimeProcess()
    {
        IngredientProcess result = null;
        foreach (var process in PossibleTypesToProcess.Values)
        {
            var temp = process.MaxBy(x => x.TimeToFinish);
            if (result is null || result.TimeToFinish < temp.TimeToFinish)
            {
                result = temp;
            }
        }
        return null;
    }
    /// <summary>
    /// Gets ingredient processes and their all information for every ingredient
    /// </summary>
    /// <returns></returns>
    public IReadOnlyDictionary<Type, List<IngredientProcess>> GetIngredientProcessesInformation() =>
        new ReadOnlyDictionary<Type, List<IngredientProcess>>(PossibleTypesToProcess);
    /// <summary>
    /// Gets string representation of default process container
    /// </summary>
    public override string ToString() => string.Format("{0}. Count: {1}", GetType().Name, PossibleTypesToProcess.Count);
    /// <summary>
    /// Gets hash code of default process container
    /// </summary>
    public override int GetHashCode() => PossibleTypesToProcess.GetHashCode();
    /// <summary>
    /// Checks equality of default process container with other specified object 
    /// </summary>
    public override bool Equals(object? obj) => obj is DefaultIngredientProcessContainer defaultProcessContainer &&
                                                PossibleTypesToProcess.AllEquals(defaultProcessContainer.PossibleTypesToProcess);
}

using PhlegmaticOne.Eatery.Lib.Extensions;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using System.Collections;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents prepared default process container for ingredient processes
/// </summary>
public class DefaultProcessContainer : IIngredientProcessContainer, IEnumerable<KeyValuePair<Type, IList<IngredientProcess>>>
{
    /// <summary>
    /// Possible ingredient types, instances of which can be operated by specified process
    /// </summary>
    private readonly IDictionary<Type, IList<IngredientProcess>> _possibleTypesToProcess;
    /// <summary>
    /// Initializes new default process container
    /// </summary>
    /// <param name="possibleTypesToProcess">Possible ingredient types, instances of which can be operated by specified process</param>
    /// <exception cref="ArgumentNullException">PossibleTypesToProcess is null</exception>
    public DefaultProcessContainer(IDictionary<Type, IList<IngredientProcess>> possibleTypesToProcess) =>
        _possibleTypesToProcess = possibleTypesToProcess ?? throw new ArgumentNullException(nameof(possibleTypesToProcess));
    /// <summary>
    /// Returns default builder for default container of specifiedingredient process type
    /// </summary>
    /// <typeparam name="TProcess">Ingredient process type</typeparam>
    public static IIngredientProcessContainerBuilder GetDefaultContainerBuilder() => new DefaultProcessContainerBuilder();
    /// <summary>
    /// Gets enumerator of default container
    /// </summary>
    public IEnumerator<KeyValuePair<Type, IList<IngredientProcess>>> GetEnumerator() => _possibleTypesToProcess.GetEnumerator();
    /// <summary>
    /// Tries to add new ingredient process for specified ingredient type
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    /// <param name="process">Instanceof process</param>
    /// <returns>False - ingredient type is already registered or instance of process is null</returns>
    public bool TryAdd<TProcess, TIngredient>(TProcess process) where TIngredient : Ingredient, new()
                                                                  where TProcess : IngredientProcess, new()
    {
        if(_possibleTypesToProcess.TryGetValue(process.GetType(), out var ingredientProcesses))
        {
            if(ingredientProcesses.Contains(process) == false)
            {
                ingredientProcesses.Add(process);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Tries to remove ingredient type and its process
    /// </summary>
    /// <typeparam name="TIngredient">Type of ingredient</typeparam>
    public bool TryRemove<TProcess, TIngredient>() where TIngredient : Ingredient, new()
                                                     where TProcess : IngredientProcess, new()
    {
        if(_possibleTypesToProcess.TryGetValue(typeof(TProcess), out var ingredientProcesses))
        {
            var fitted = ingredientProcesses.FirstOrDefault(p => p.CurrentIngredientType == typeof(TProcess));
            if(fitted != null)
            {
                ingredientProcesses.Remove(fitted);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Tries to update process for ingredient type
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    /// <typeparam name="TProcess">Ingredient process type</typeparam>
    /// <param name="process">New process to set</param>
    public bool TryUpdate<TIngredient, TProcess>(TProcess process)
                  where TProcess : IngredientProcess, new()
                  where TIngredient : Ingredient, new()
    {
        if (process is null) return false;
        if(_possibleTypesToProcess.TryGetValue(typeof(TProcess), out var ingredientProcesses))
        {
            var fitted = ingredientProcesses.FirstOrDefault(p => p.CurrentIngredientType == typeof(TIngredient));
            if(fitted is not null)
            {
                ingredientProcesses.Remove(fitted);
                ingredientProcesses.Add(process);
                return true;
            }
        }
        return false;
    }
    public TProcess GetProcess<TProcess, TIngredient>()
                         where TProcess : IngredientProcess, new()
                         where TIngredient : Ingredient, new()
    {
        if (_possibleTypesToProcess.TryGetValue(typeof(TProcess), out var processes))
        {
            return processes.FirstOrDefault(p => p.CurrentIngredientType == typeof(TIngredient)) as TProcess;
        }
        return null;
    }
    /// <summary>
    /// Gets enumerator of default container
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_possibleTypesToProcess).GetEnumerator();
    /// <summary>
    /// Gets string representation of default process container
    /// </summary>
    public override string ToString() => string.Format("Count: {0}", _possibleTypesToProcess.Count);
    /// <summary>
    /// Gets hash code of default process container
    /// </summary>
    public override int GetHashCode() => _possibleTypesToProcess.GetHashCode();
    /// <summary>
    /// Checks equality of default process container with other specified object 
    /// </summary>
    public override bool Equals(object? obj) => obj is DefaultProcessContainer defaultProcessContainer &&
                                                _possibleTypesToProcess.AllEquals(defaultProcessContainer._possibleTypesToProcess);


}

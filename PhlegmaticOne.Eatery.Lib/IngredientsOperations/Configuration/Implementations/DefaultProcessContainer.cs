using PhlegmaticOne.Eatery.Lib.Extensions;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using System.Collections;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents prepared default process container for ingredient processes
/// </summary>
public class DefaultProcessContainer : IProcessContainer, IEnumerable<KeyValuePair<Type, DomainProductProcess>>
{
    /// <summary>
    /// Possible ingredient types, instances of which can be operated by specified process
    /// </summary>
    private readonly IDictionary<Type, DomainProductProcess> _possibleTypesToProcess;
    /// <summary>
    /// Initializes new default process container
    /// </summary>
    /// <param name="possibleTypesToProcess">Possible ingredient types, instances of which can be operated by specified process</param>
    /// <exception cref="ArgumentNullException">PossibleTypesToProcess is null</exception>
    public DefaultProcessContainer(IDictionary<Type, DomainProductProcess> possibleTypesToProcess) =>
        _possibleTypesToProcess = possibleTypesToProcess ?? throw new ArgumentNullException(nameof(possibleTypesToProcess));
    /// <summary>
    /// Returns default builder for default container of specifiedingredient process type
    /// </summary>
    /// <typeparam name="TProcess">Ingredient process type</typeparam>
    public static IProcessContainerBuilder<TProcess> GetDefaultContainerBuilder<TProcess>()
                                                  where TProcess : DomainProductProcess, new() =>
                  new DefaultProcessContainerBuilder<TProcess>(new DefaultProcessBuilder<TProcess>());
    /// <summary>
    /// Gets enumerator of default container
    /// </summary>
    public IEnumerator<KeyValuePair<Type, DomainProductProcess>> GetEnumerator() => _possibleTypesToProcess.GetEnumerator();

    /// <summary>
    /// Gets process for ingredient type
    /// </summary>
    /// <typeparam name="TIngredient">Type of ingredient</typeparam>
    public DomainProductProcess GetProcessOf<TIngredient>() where TIngredient : DomainProductToPrepare
    {
        if (_possibleTypesToProcess.TryGetValue(typeof(TIngredient), out var process))
        {
            return process;
        }
        return null;
    }
    /// <summary>
    /// Tries to add new ingredient process for specified ingredient type
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    /// <param name="process">Instanceof process</param>
    /// <returns>False - ingredient type is already registered or instance of process is null</returns>
    internal bool TryAdd<TIngredient>(DomainProductProcess process) where TIngredient : Ingredient, new() =>
        process is null || _possibleTypesToProcess.TryAdd(typeof(TIngredient), process);
    /// <summary>
    /// Tries to remove ingredient type and its process
    /// </summary>
    /// <typeparam name="TIngredient">Type of ingredient</typeparam>
    internal bool TryRemove<TIngredient>() where TIngredient : Ingredient, new() =>
        _possibleTypesToProcess.Remove(typeof(TIngredient));
    /// <summary>
    /// Tries to update process for ingredient type
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    /// <typeparam name="TProcess">Ingredient process type</typeparam>
    /// <param name="process">New process to set</param>
    internal bool TryUpdate<TIngredient, TProcess>(TProcess process)
                  where TProcess : DomainProductProcess, new()
                  where TIngredient : Ingredient, new()
    {
        if (process is null) return false;
        if (_possibleTypesToProcess.ContainsKey(typeof(TIngredient)))
        {
            _possibleTypesToProcess.Remove(typeof(TIngredient));
            _possibleTypesToProcess.Add(typeof(TIngredient), process);
            return true;
        }
        return false;
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

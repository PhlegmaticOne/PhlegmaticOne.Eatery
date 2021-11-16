﻿using PhlegmaticOne.Eatery.Lib.Extensions;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents contract for ingredient process container
/// </summary>
public abstract class IngredientProcessContainerBase
{
    /// <summary>
    /// Possible ingredient types, instances of which can be operated by specified process
    /// </summary>
    internal readonly IDictionary<Type, IList<IngredientProcess>> PossibleTypesToProcess;
    /// <summary>
    /// Initializes new default process container
    /// </summary>
    /// <param name="possibleTypesToProcess">Possible ingredient types, instances of which can be operated by specified process</param>
    /// <exception cref="ArgumentNullException">PossibleTypesToProcess is null</exception>
    protected IngredientProcessContainerBase(IDictionary<Type, IList<IngredientProcess>> possibleTypesToProcess) =>
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
    public IReadOnlyDictionary<Type, IList<IngredientProcess>> GetIngredientProcessesInformation() =>
        new ReadOnlyDictionary<Type, IList<IngredientProcess>>(PossibleTypesToProcess);
    /// <summary>
    /// Gets string representation of default process container
    /// </summary>
    public override string ToString() => string.Format("Count: {0}", PossibleTypesToProcess.Count);
    /// <summary>
    /// Gets hash code of default process container
    /// </summary>
    public override int GetHashCode() => PossibleTypesToProcess.GetHashCode();
    /// <summary>
    /// Checks equality of default process container with other specified object 
    /// </summary>
    public override bool Equals(object? obj) => obj is DefaultProcessContainer defaultProcessContainer &&
                                                PossibleTypesToProcess.AllEquals(defaultProcessContainer.PossibleTypesToProcess);
}

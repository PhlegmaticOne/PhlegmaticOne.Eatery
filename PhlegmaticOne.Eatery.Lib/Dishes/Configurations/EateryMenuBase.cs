using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.Recipies;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Dishes;
/// <summary>
/// Represents base eatery menu for other menues
/// </summary>
public abstract class EateryMenuBase
{
    /// <summary>
    /// Recipies of dishes
    /// </summary>
    [JsonProperty]
    internal Dictionary<string, Recipe> Recipies;
    /// <summary>
    /// Initializes new EateryMenuBase instance
    /// </summary>
    protected EateryMenuBase() => Recipies = new Dictionary<string, Recipe>();
    /// <summary>
    /// Initializes new EateryMenuBase instance
    /// </summary>
    /// <param name="recipies">Specified recipies</param>
    [JsonConstructor]
    protected EateryMenuBase(Dictionary<string, Recipe> recipies) => Recipies = recipies;
    /// <summary>
    /// Amount of recipies in menu
    /// </summary>
    public int Count => Recipies.Count;
    /// <summary>
    /// Gets all recipies from menu
    /// </summary>
    /// <returns></returns>
    public IReadOnlyDictionary<string, Recipe> GetAllRecipes() => new ReadOnlyDictionary<string, Recipe>(Recipies);
    /// <summary>
    /// Tries to add new recipe in menu and connect it with specified name of dish
    /// </summary>
    /// <returns>True - connection was successful</returns>
    internal virtual bool TryConnectNameOfDishWithRecipe(string nameOfDish, Recipe recipe)
    {
        if (Recipies.ContainsKey(nameOfDish))
        {
            return false;
        }
        Recipies.Add(nameOfDish, recipe);
        return true;
    }
    /// <summary>
    /// Tries to remove recipe from menu by its connected dish name
    /// </summary>
    /// <returns>True - removing was successful</returns>
    internal virtual bool TryRemoveRecipe(string nameOfDish)
    {
        if (Recipies.ContainsKey(nameOfDish))
        {
            Recipies.Remove(nameOfDish);
            return true;
        }
        return false;
    }
    /// <summary>
    /// Tries to retirn recipe by its connected dish name
    /// </summary>
    /// <returns>True - returning was successful</returns>
    internal virtual bool TryGetRecipe(string nameOfDish, out Recipe recipe)
    {
        if (Recipies.TryGetValue(nameOfDish, out Recipe recipeFinded))
        {
            recipe = recipeFinded;
            return true;
        }
        else
        {
            recipe = null;
            return false;
        }
    }
    public override bool Equals(object? obj) => obj is EateryMenuBase eateryMenu &&
                                                eateryMenu.Recipies.Except(Recipies).Any() == false;
    public override int GetHashCode() => Recipies.GetHashCode();
    public override string ToString() => string.Format("{0}. Count: {1}", GetType().Name, Count);
}

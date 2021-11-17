using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.Recipies;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Dishes;

public abstract class EateryMenuBase
{
    [JsonProperty]
    internal Dictionary<string, Recipe> Recipies;
    protected EateryMenuBase() => Recipies = new Dictionary<string, Recipe>();
    [JsonConstructor]
    protected EateryMenuBase(Dictionary<string, Recipe> recipies) => Recipies = recipies;
    internal virtual bool TryConnectNameOfDishWithRecipe(string nameOfDish, Recipe recipe)
    {
        if (Recipies.ContainsKey(nameOfDish))
        {
            return false;
        }
        Recipies.Add(nameOfDish, recipe);
        return true;
    }
    public IReadOnlyDictionary<string, Recipe> GetAllRecipes() => new ReadOnlyDictionary<string, Recipe>(Recipies);

    internal virtual bool TryRemoveRecipe(string nameOfDish)
    {
        if (Recipies.ContainsKey(nameOfDish))
        {
            Recipies.Remove(nameOfDish);
            return true;
        }
        return false;
    }

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
}

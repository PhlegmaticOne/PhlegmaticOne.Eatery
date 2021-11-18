using PhlegmaticOne.Eatery.Lib.Recipies;

namespace PhlegmaticOne.Eatery.Lib.Dishes;
/// <summary>
/// Represents default eatery menu
/// </summary>
public class EateryMenu : EateryMenuBase
{
    /// <summary>
    /// Initializes new EateryMenu instance
    /// </summary>
    public EateryMenu() { }
    /// <summary>
    /// Initializes new EateryMenu instance
    /// </summary>
    /// <param name="recipies">Specified recipies</param>
    [Newtonsoft.Json.JsonConstructor]
    internal EateryMenu(Dictionary<string, Recipe> recipies) : base(recipies) { }
}
using PhlegmaticOne.Eatery.Lib.Recipies;

namespace PhlegmaticOne.Eatery.Lib.Dishes;

public class EateryMenu : EateryMenuBase
{
    public EateryMenu() { }
    [Newtonsoft.Json.JsonConstructor]
    public EateryMenu(Dictionary<string, Recipe> recipies) : base(recipies) { }
}

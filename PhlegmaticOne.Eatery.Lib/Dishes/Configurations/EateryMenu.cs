using PhlegmaticOne.Eatery.Lib.Recipies;

namespace PhlegmaticOne.Eatery.Lib.Dishes.Configurations;

public class EateryMenu : EateryMenuBase
{
    public EateryMenu() { }
    public EateryMenu(IDictionary<string, Recipe> recipies) : base(recipies) { }
}

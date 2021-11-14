using PhlegmaticOne.Eatery.Lib.Recipies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.Lib.Dishes.Configurations;

public class EateryMenu
{
    private readonly Dictionary<string, Recipe> _recipies;
    public EateryMenu() => _recipies = new();
}

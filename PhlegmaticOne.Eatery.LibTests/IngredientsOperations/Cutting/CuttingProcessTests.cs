using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using System;
using System.Linq;

namespace PhlegmaticOne.Eatery.Lib.Models.Ingredients.Tests;

[TestClass()]
public class CuttingProcessTests
{
    [TestMethod()]
    public void CutToTest()
    {
        var cutting = new CuttingProcess(TimeSpan.FromSeconds(40), new Money(10, "USD"));
        var cucumber = new Cucumber(130, 45);
        var cuttedCucumber = cutting.CutTo(cucumber, 10);
        Assert.IsTrue(cuttedCucumber.Count() == 5);
        Assert.IsTrue(cuttedCucumber.First().Value == 10);
        Assert.IsTrue(cuttedCucumber.Last().Value == 5);
    }
}

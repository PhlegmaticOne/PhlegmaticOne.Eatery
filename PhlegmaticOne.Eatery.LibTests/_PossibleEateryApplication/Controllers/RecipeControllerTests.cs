using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Recipies;
using System;
using System.Reflection;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication.Tests;

[TestClass()]
public class RecipeControllerTests
{
    [TestMethod()]
    public void GetRecipeBuilderTest()
    {
        ///Конструктор internal, поэтому создать объект простым вызовом конструктора нельзя
        var type = typeof(RecipeController);
        var recipeController = type.Assembly.CreateInstance(
                             type.FullName, true, BindingFlags.Instance | BindingFlags.NonPublic,
                             null, new object[] {null , null, null}, null, null) as RecipeController;
        var worker = new Chief("S");
        var getRecipeBuilderRequest = new DefaultApplicationRequest<string>(worker, "VegetableSalad");
        var getRecipeBuilderRespond = recipeController.GetRecipeBuilder(getRecipeBuilderRequest);
        Assert.IsInstanceOfType(getRecipeBuilderRespond.RespondResult1, typeof(DefaultRecipeBuilder));
    }
}

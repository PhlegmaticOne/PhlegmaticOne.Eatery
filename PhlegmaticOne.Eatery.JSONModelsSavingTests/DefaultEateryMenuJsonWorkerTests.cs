using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Recipies;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.JSONModelsSaving.Tests;

[TestClass()]
public class DefaultEateryMenuJsonWorkerTests
{
    private const string DIRECTORY_WITH_TESTED_FILES_PATH = @"C:\Users\lolol\source\repos\PhlegmaticOne.Eatery\PhlegmaticOne.Eatery.JSONModelsSavingTests\TestFiles\";
    private const string WORKERS_FILE_NAME = @"menu.json";
    private static DefaultEateryMenuJsonWorker _menuJsonWorker;
    private static RecipeController? _recipeController;
    private static EateryMenuController? _eateryMenuController;

    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
        _menuJsonWorker = new(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME);
        var ingredientProcessesContainer = DefaultProcessContainer.GetDefaultContainerBuilder()
            .ConfigureProcess<CuttingProcess, DefaultProcessBuilder<CuttingProcess>>(builder =>
            {
                builder.CanProcess<Cucumber>().WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                builder.CanProcess<Tomato>().WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                builder.CanProcess<Olive>().WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
            })
            .ConfigureProcess<AddingProcess, DefaultProcessBuilder<AddingProcess>>(builder =>
            {
                builder.CanProcess<Cucumber>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromSeconds(10));
                builder.CanProcess<Tomato>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromSeconds(20));
                builder.CanProcess<Olive>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromSeconds(30));
                builder.CanProcess<OliveOil>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromSeconds(40));
            })
            .Build();

        var intermediateProcessContainer =
            DefaultIntermediateProcessContainer.GetDefaultIntermediateProcessContainerBuilder()
            .ConfigureProcess<MixingProcess, DefaultIntermadiateProcessBuilder<MixingProcess>>(builder =>
            {
                builder.DishMayContain<Cucumber>().DishMayContain<Tomato>()
                       .WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                builder.SetDefaultProcess(new Money(20, "RUB"), TimeSpan.FromMinutes(1));
            })
            .Build();
        var eateryMenu = new EateryMenu();
        var recipeControllerType = typeof(RecipeController);
        _recipeController = recipeControllerType.Assembly.CreateInstance(
                             recipeControllerType.FullName, true, BindingFlags.Instance | BindingFlags.NonPublic,
                             null, new object[] { ingredientProcessesContainer, intermediateProcessContainer, eateryMenu }, null, null) as RecipeController;

        ///Конструктор internal, поэтому создать объект простым вызовом конструктора нельзя
        var type = typeof(EateryMenuController);
        _eateryMenuController = type.Assembly.CreateInstance(
                             type.FullName, true, BindingFlags.Instance | BindingFlags.NonPublic,
                             null, new object[] { eateryMenu }, null, null) as EateryMenuController;

        var worker = new Chief("S");
        var getRecipeBuilderRequest = new DefaultApplicationRequest<string>(worker, "VegetableSalad");
        var getRecipeBuilderRespond = _recipeController.GetRecipeBuilder(getRecipeBuilderRequest);
        var recipe = getRecipeBuilderRespond.RespondResult1
                        .Configure<DefaultRecipeIngredientTypesConfiguration, DefaultProcessSequenceBuilder>(
                        recipeBuilder =>
                        {
                            recipeBuilder.NeedIngredient<Cucumber>().InWeightOf(100);
                            recipeBuilder.NeedIngredient<Tomato>().InWeightOf(100);
                            recipeBuilder.NeedIngredient<Olive>().InWeightOf(30);
                            recipeBuilder.NeedIngredient<OliveOil>().InWeightOf(50);
                        },
                        sequenceBuilder =>
                        {
                            sequenceBuilder.InsertInSequence<AddingProcess, Cucumber>();
                            sequenceBuilder.InsertInSequence<CuttingProcess, Cucumber>();
                            sequenceBuilder.InsertInSequence<AddingProcess, Tomato>();
                            sequenceBuilder.InsertInSequence<CuttingProcess, Tomato>();
                            sequenceBuilder.InsertInSequence<AddingProcess, Olive>();
                            sequenceBuilder.InsertInSequence<CuttingProcess, Olive>();
                            sequenceBuilder.InsertInSequence<AddingProcess, OliveOil>();
                            sequenceBuilder.InsertInSequence<MixingProcess>();
                        }
                    )
                    .Create();

        var addingRecipeRequest = new DefaultApplicationRequest<Recipe>(worker, recipe);
        var addingRecipeRespond = _eateryMenuController.AddRecipeInMenu(addingRecipeRequest);
    }
    [TestMethod]
    public async Task ASaveIngredientsTest()
    {
        await _menuJsonWorker.SaveAsync(_eateryMenuController.GetEateryMenu(
              EmptyApplicationRequest.Empty(new Cook("S"))).RespondResult1);
        Assert.IsTrue(File.Exists(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME));
    }
    [TestMethod()]
    public async Task BLoadIngredientsTest()
    {
        var recipies = await _menuJsonWorker.LoadAsync<EateryMenu>();
        Assert.IsInstanceOfType(recipies, typeof(EateryMenu));
    }
}

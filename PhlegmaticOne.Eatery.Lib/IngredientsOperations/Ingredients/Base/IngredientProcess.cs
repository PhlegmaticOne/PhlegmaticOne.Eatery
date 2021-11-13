﻿using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents base operation over any ingredient
/// </summary>
public abstract class IngredientProcess : DomainProductProcess
{
    protected IngredientProcess()
    {
    }

    protected IngredientProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price)
    {
    
    }
    internal Type CurrentIngredientType { get; set; }
    public abstract IEnumerable<Ingredient> ProcessOver(Ingredient ingredient);
    
    /// <summary>
    /// Check equality of ingredient process with specified object
    /// </summary>
    public override bool Equals(object? obj) => obj is DomainProductProcess ingredientProcess &&
                                                TimeToFinish == ingredientProcess.TimeToFinish &&
                                                Price == ingredientProcess.Price;
    /// <summary>
    /// Gets hash code of ingredient process
    /// </summary>
    override public int GetHashCode() => TimeToFinish.Milliseconds ^ (int)Price.Amount ^ Price.CurrencyCode.GetHashCode();
    /// <summary>
    /// Gets string representation of ingredient process
    /// </summary>
    /// <returns></returns>
    public override string ToString() => string.Format("Process is {0}. Price: {1}. Time to finish: {2}",
                                                        GetType().Name, Price, TimeToFinish.ToString());
}
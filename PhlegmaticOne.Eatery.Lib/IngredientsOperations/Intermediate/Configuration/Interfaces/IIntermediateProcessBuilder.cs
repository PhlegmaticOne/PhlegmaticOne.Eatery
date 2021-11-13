using PhlegmaticOne.Eatery.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public interface IIntermediateProcessBuilder<TProcess> where TProcess : IntermediateProcess, new()
{
    void SetDefaultProcess(Money money, TimeSpan timeSpan);
    IIntermediateProcessBuilder<TProcess> DishMayContain<TIngredient>();
    /// <summary>
    /// Sets cost to a building ingredient process
    /// </summary>
    /// <param name="money">Price of process</param>
    /// <returns>Current instance of builder</returns>
    IIntermediateProcessBuilder<TProcess> WithCost(Money money);
    /// <summary>
    /// Sets duration of ingredient process
    /// </summary>
    /// <param name="timeToFinish">Specified duration</param>
    /// <returns>Current instance of process builder</returns>
    void WithTimeToFinish(TimeSpan timeToFinish);
    IList<TProcess> Build();
}

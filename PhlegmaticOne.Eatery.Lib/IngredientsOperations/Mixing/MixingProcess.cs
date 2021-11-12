using PhlegmaticOne.Eatery.Lib.Extensions;
using PhlegmaticOne.Eatery.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public class MixingProcess : DomainProductProcess, IMixingProcess
{
    public MixingProcess()
    {
    }

    public MixingProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price)
    {
    }

    public void Mix(IEnumerable<DomainProductToPrepare> productsToPrepare) => productsToPrepare.ToList().Shuffle();
}

using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public interface IRecipeProcessSequenceBuilder
{
    IRecipeProcessSequenceBuilder InsertInSequence<TIngredient>()
                                   where TIngredient : DomainProductToPrepare, new();
    IRecipeProcessSequenceBuilder SetSource(IProcessContainer processContainer);
    Queue<DomainProductProcess> BuildRecipeSequence();
}

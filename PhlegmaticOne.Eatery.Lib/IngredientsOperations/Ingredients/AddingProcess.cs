using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents adding of ingredients process
/// </summary>
public class AddingProcess : IngredientProcess, IEquatable<AddingProcess>
{
    /// <summary>
    /// Initializes new AddingProcesss instance
    /// </summary>
    public AddingProcess() { }
    /// <summary>
    /// Initializes new AddingProcesss instance
    /// </summary>
    /// <param name="timeToFinish">Specified duration</param>
    /// <param name="price">Specified price</param>
    [Newtonsoft.Json.JsonConstructor]
    public AddingProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price) { }

    public bool Equals(AddingProcess? other) => base.Equals(other);

    /// <summary>
    /// Updates prepating dish
    /// </summary>
    internal override void Update(DishBase dish, double weight)
    {
        dish.Price += Price * 1.1;
        dish.Weight += weight;
    }

    public override bool Equals(object obj) => Equals(obj as AddingProcess);

    public override int GetHashCode() => base.GetHashCode();
}

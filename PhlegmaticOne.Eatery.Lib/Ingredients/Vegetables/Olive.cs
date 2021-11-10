namespace PhlegmaticOne.Eatery.Lib.Ingredients
{
    /// <summary>
    /// Represents olive vegetable
    /// </summary>
    public class Olive : Ingredient, IEquatable<Olive>
    {
        /// <summary>
        /// Initializes new olive instance
        /// </summary>
        public Olive() { }
        /// <summary>
        /// Initialzies new olive instance
        /// </summary>
        /// <param name="weight">Specified weight</param>
        /// <param name="value">Specified value</param>
        public Olive(double weight, double value) : base(weight, value) { }
        /// <summary>
        /// Checks equality of olive with other specified olive
        /// </summary>
        public bool Equals(Olive? other) => base.Equals(other);
        /// <summary>
        /// Check equality of olive with other specified olive
        /// </summary>
        public override bool Equals(object? obj) => Equals(obj as Olive);
        /// <summary>
        /// Gets hash code of olive
        /// </summary>
        public override int GetHashCode() => base.GetHashCode();
    }
}

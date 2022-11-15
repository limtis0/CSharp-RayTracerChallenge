namespace RT.Source.Rays
{
    public class Intersection : IComparable<Intersection>
    {
        public readonly float T;
        public readonly object Figure;

        public Intersection(float T, object Figure)
        {
            this.T = T;
            this.Figure = Figure;
        }

        public int CompareTo(Intersection? other) => other is null ? throw new ArgumentException("Comparing to null") : T.CompareTo(other.T);

        public override string ToString() => $"Intersection(T:{T}, Figure:{Figure})";
    }
}

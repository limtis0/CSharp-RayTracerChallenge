namespace RT.Source.Rays
{
    public class Intersections : SortedList<Intersection>
    {
        public Intersection? Hit() => list.FirstOrDefault(x => x.T >= 0);
    }
}

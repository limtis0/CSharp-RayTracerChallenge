namespace RT.Source.Rays
{
    public class Intersections : SortedList<Intersection>
    {
        public Intersection? Hit() => list.Where(x => x.T >= 0).FirstOrDefault();
    }
}

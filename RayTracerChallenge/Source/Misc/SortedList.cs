using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RayTracerTests")]
namespace RT.Source
{
    public class SortedList<T> where T : IComparable<T>
    {
        internal List<T> list = new();

        public T this[int i]
        {
            get => list[i];
        }

        public int Count { get => list.Count; }

        public SortedList() { }

        public SortedList(List<T> input)
        {
            input.Sort();
            list = input;
        }

        public void Insert(T item)
        {
            // If list is empty
            if (Count == 0)
            {
                list.Add(item);
            }
            // If item is bigger than the last element
            else if (list[^1].CompareTo(item) <= 0)
            {
                list.Add(item);
            }
            // If item is less than the first element
            else if (list[0].CompareTo(item) >= 0)
            {
                list.Insert(0, item);
            }
            else
            {
                /* Binary search returns:
                 * The zero-based index of item in the sorted List<T>, if item is found;
                 * otherwise, a negative number that is the bitwise complement of the index
                 * of the next element that is larger than item
                 * or, if there is no larger element, the bitwise complement of Count. */
                int index = list.BinarySearch(item);
                if (index < 0)
                    index = ~index;
                list.Insert(index, item);
            }
        }

        public void Insert(params T[] items)
        {
            foreach (T item in items)
                Insert(item);
        }

        public void Remove(T item) => list.Remove(item);

        public void RemoveAt(int index) => list.RemoveAt(index);
    }
}

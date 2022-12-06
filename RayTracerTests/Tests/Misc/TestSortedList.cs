using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source;
using System.Collections.Generic;

namespace RayTracerTests
{
    [TestClass]
    public class TestSortedList
    {
        [TestMethod]
        public void TestInsertion()
        {
            // From unsorted list
            SortedList<double> list = new();

            list.Insert(-1.234);
            list.Insert(0);
            list.Insert(-99.432);
            list.Insert(111, 0.1, 83.53);

            List<double> expected = new() { -99.432, -1.234, 0, 0.1, 83.53, 111 };

            CollectionAssert.AreEqual(list.list, expected);


            // From a sorted list
            SortedList<double> from_expected = new(expected);

            CollectionAssert.AreEqual(from_expected.list, expected);
        }

        [TestMethod]
        public void TestRemoval()
        {
            List<int> expected = new() { -2, -1, 0, 1, 2, 3 };
            SortedList<int> list = new(expected);

            expected.Remove(0);
            list.Remove(0);

            expected.RemoveAt(2);
            list.RemoveAt(2);

            CollectionAssert.AreEqual(list.list, expected);
        }
    }
}

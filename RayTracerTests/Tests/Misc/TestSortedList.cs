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
            SortedList<float> list = new();

            list.Insert(-1.234f);
            list.Insert(0);
            list.Insert(-99.432f);
            list.Insert(111f, 0.1f, 83.53f);

            List<float> expected = new() { -99.432f, -1.234f, 0, 0.1f, 83.53f, 111f };

            CollectionAssert.AreEqual(list.list, expected);


            // From a sorted list
            SortedList<float> from_expected = new(expected);

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

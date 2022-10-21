using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Matrices;

namespace RayTracerTests
{
    [TestClass]
    public class TestMatrix
    {
        [TestMethod]
        public void TestCreation()
        {
            float[,] m1 = new float[,] { { 1, 2 }, { 3, 4 } };
            Matrix matrix1 = new(m1);
            Assert.AreEqual(m1, matrix1.matrix);
        }

        [TestMethod]
        public void TestEquality()
        {
            Matrix matrix1 = new(new float[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new(new float[,] { { 1, 2 }, { 3, 4 } });
            Assert.AreEqual(matrix1, matrix2);

            Matrix matrix3 = new(new float[,] { { 4, 3 }, { 2, 1 } });
            Assert.AreNotEqual(matrix2, matrix3);
        }

        [TestMethod]
        public void TestMultiplyByMatrix()
        {
            Matrix m1 = new(new float[,]
            {
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 8, 7, 6},
                {5, 4, 3, 2},
            });
            
            Matrix m2 = new(new float[,]
            {
                {-2, 1, 2, 3},
                {3, 2, 1, -1},
                {4, 3, 6, 5},
                {1, 2, 7, 8},
            });

            Matrix expected = new(new float[,]
            {
                {20, 22, 50, 48},
                {44, 54, 114, 108},
                {40, 58, 110, 102},
                {16, 26, 46, 42},
            });

            Assert.AreEqual(m1 * m2, expected);
        }

        [TestMethod]
        public void TestMultiplyByDifferentSized()
        {
            Matrix m1 = new(new float[,]
            {
                {1, 2, 3},
                {4, 5, 6}
            });
            
            Matrix m2 = new(new float[,]
            {
                {10, 11},
                {20, 21},
                {30, 31}
            });

            Matrix expected = new(new float[,]
            {
                {140, 146},
                {320, 335}
            });

            Assert.AreEqual(m1*m2, expected);
        }

        [TestMethod]
        public void TestMultiplyByTuple()
        {
            Matrix m = new(new float[,]
            {
                {1, 2, 3, 4},
                {2, 4, 4, 2},
                {8, 6, 4, 1},
                {0, 0, 0, 1}
            });

            RT.Source.Vectors.Tuple t = new(1, 2, 3, 1);
            RT.Source.Vectors.Tuple expected = new(18, 24, 33, 1);

            Assert.AreEqual(m * t, expected);
        }

        [TestMethod]
        public void TestIdentity()
        {
            Matrix m = new(new float[,] 
            {
                {0, 1, 2, 4},
                {1, 2, 4, 8},
                {2, 4, 8, 16},
                {4, 8, 16, 32}
            });
            Assert.AreEqual(m * Matrix.Identity(4), m);

            RT.Source.Vectors.Tuple t = new(1, 2, 3, 1);
            Assert.AreEqual(Matrix.Identity(4) * t, t);
        }

        [TestMethod]
        public void TestTransposed()
        {
            Matrix m = new(new float[,]
            {
                {0, 9, 3, 0},
                {9, 8, 0, 8},
                {1, 8, 5, 3},
                {0, 0, 5, 8}
            });

            Matrix transposed = new(new float[,]
            {
                {0, 9, 1, 0},
                {9, 8, 8, 0},
                {3, 0, 5, 5},
                {0, 8, 3, 8}
            });

            Assert.AreEqual(m.Transposed(), transposed);
            
            // For Identity
            Assert.AreEqual(Matrix.Identity(4), Matrix.Identity(4).Transposed());
        }
    }
}
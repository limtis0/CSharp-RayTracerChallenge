using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Matrices;
using RT.Source;

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

        [TestMethod]
        public void TestSubmatrix()
        {
            // Submatrix of 3x3
            Matrix m3x3 = new(new float[,]
            {
                {1, 5, 0},
                {-3, 2, 7},
                {0, 6, -3}
            });

            Matrix sub3x3 = new(new float[,]
            {
                {-3, 2},
                {0, 6}
            });

            Assert.AreEqual(m3x3.Submatrix(0, 2), sub3x3);


            // Submatrix of 4x4
            Matrix m4x4 = new(new float[,]
            {
                {-6, 1, 1, 6},
                {-8, 5, 8, 6},
                {-1, 0, 8, 2},
                {-7, 1, -1, 1}
            });

            Matrix sub4x4 = new(new float[,]
            {
                {-6, 1, 6},
                {-8, 8, 6},
                {-7, -1, 1}
            });

            Assert.AreEqual(m4x4.Submatrix(2, 1), sub4x4);
        }

        [TestMethod]
        public void TestMinors()
        {
            Matrix m = new(new float[,]
            {
                {3, 5, 0},
                {2, -1, -7},
                {6, -1, 5}
            });

            Assert.AreEqual(m.Minor(1, 0), 25);
        }

        [TestMethod]
        public void TestCofactor()
        {
            Matrix m = new(new float[,]
            {
                {3, 5, 0},
                {2, -1, -7},
                {6, -1, 5}
            });

            Assert.AreEqual(m.Minor(0, 0), -12);
            Assert.AreEqual(m.Cofactor(0, 0), -12);

            Assert.AreEqual(m.Minor(1, 0), 25);
            Assert.AreEqual(m.Cofactor(1, 0), -25);
        }

        [TestMethod]
        public void TestDeterminant()
        {
            // Determinant of 3x3 matrix
            Matrix m3x3 = new(new float[,]
            {
                {1, 2, 6},
                {-5, 8, -4},
                {2, 6, 4}
            });

            Assert.AreEqual(m3x3.Cofactor(0, 0), 56);
            Assert.AreEqual(m3x3.Cofactor(0, 1), 12);
            Assert.AreEqual(m3x3.Cofactor(0, 2), -46);
            Assert.AreEqual(m3x3.Determinant(), -196);


            // Determinant of 4x4 matrix
            Matrix m4x4 = new(new float[,]
            {
                {-2, -8, 3, 5},
                {-3, 1, 7, 3},
                {1, 2, -9, 6},
                {-6, 7, 7, -9}
            });

            Assert.AreEqual(m4x4.Cofactor(0, 0), 690);
            Assert.AreEqual(m4x4.Cofactor(0, 1), 447);
            Assert.AreEqual(m4x4.Cofactor(0, 2), 210);
            Assert.AreEqual(m4x4.Cofactor(0, 3), 51);
            Assert.AreEqual(m4x4.Determinant(), -4071);
        }

        [TestMethod]
        public void TestInverse()
        {
            // First
            Matrix m1 = new(new float[,]
            {
                {-5, 2, 6, -8},
                {1, -5, 1, 8},
                {7, 7, -6, -7},
                {1, -3, 7, 4}
            });

            Matrix inverse1 = new(new float[,]
            {
                {0.21805f, 0.45113f, 0.24060f, -0.04511f},
                {-0.80827f, -1.45677f, -0.44361f, 0.52068f},
                {-0.07895f, -0.22368f, -0.05263f, 0.19737f},
                {-0.52256f, -0.81391f,  -0.30075f, 0.30639f}
            });

            Assert.AreEqual(m1.Determinant(), 532);
            Assert.AreEqual(m1.Cofactor(2, 3), -160);
            Assert.IsTrue(Calc.Equals(inverse1.matrix[3, 2], -160 / 532f));
            Assert.IsTrue(Calc.Equals(inverse1.matrix[2, 3], 105 / 532f));
            Assert.AreEqual(m1.Inverse(), inverse1);


            // Second
            Matrix m2 = new(new float[,]
            {
                {8, -5, 9, 2},
                {7, 5, 6, 1},
                {-6, 0, 9, 6},
                {-3, 0, -9, -4}
            });

            Matrix inverse2 = new(new float[,]
            {
                {-0.15385f, -0.15385f, -0.28205f, -0.53846f},
                {-0.07692f, 0.12308f, 0.02564f, 0.03077f},
                {0.35897f, 0.35897f, 0.43590f, 0.92308f},
                {-0.69231f, -0.69231f, -0.76923f, -1.92308f}
            });

            Assert.AreEqual(m2.Inverse(), inverse2);


            // Third
            Matrix m3 = new(new float[,]
            {
                {9, 3, 0, 9},
                {-5, -2, -6, -3},
                {-4, 9, 6, 4},
                {-7, 6, 6, 2}
            });

            Matrix inverse3 = new(new float[,]
            {
                {-0.04074f, -0.07778f, 0.14444f, -0.22222f},
                {-0.07778f, 0.03333f, 0.36667f, -0.33333f},
                {-0.02901f, -0.14630f, -0.10926f, 0.12963f},
                {0.17778f, 0.06667f, -0.26667f, 0.33333f}
            });

            Assert.AreEqual(m3.Inverse(), inverse3);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source;
using RT.Source.Matrices;
using RT.Source.Vectors;
using static System.Math;

namespace RayTracerTests
{
    [TestClass]
    public class TestMatrix
    {
        #region Base

        [TestMethod]
        public void TestCreation()
        {
            double[,] m1 = new double[,] { { 1, 2 }, { 3, 4 } };
            Matrix matrix1 = new(m1);
            Assert.AreEqual(m1, matrix1.matrix);
        }

        [TestMethod]
        public void TestEquality()
        {
            Matrix matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Assert.AreEqual(matrix1, matrix2);

            Matrix matrix3 = new(new double[,] { { 4, 3 }, { 2, 1 } });
            Assert.AreNotEqual(matrix2, matrix3);
        }

        [TestMethod]
        public void TestMultiplyByMatrix()
        {
            Matrix m1 = new(new double[,]
            {
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 8, 7, 6},
                {5, 4, 3, 2},
            });

            Matrix m2 = new(new double[,]
            {
                {-2, 1, 2, 3},
                {3, 2, 1, -1},
                {4, 3, 6, 5},
                {1, 2, 7, 8},
            });

            Matrix expected = new(new double[,]
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
            Matrix m1 = new(new double[,]
            {
                {1, 2, 3},
                {4, 5, 6}
            });

            Matrix m2 = new(new double[,]
            {
                {10, 11},
                {20, 21},
                {30, 31}
            });

            Matrix expected = new(new double[,]
            {
                {140, 146},
                {320, 335}
            });

            Assert.AreEqual(m1 * m2, expected);
        }

        [TestMethod]
        public void TestMultiplyByTuple()
        {
            Matrix m = new(new double[,]
            {
                {1, 2, 3, 4},
                {2, 4, 4, 2},
                {8, 6, 4, 1},
                {0, 0, 0, 1}
            });

            Tuple t = new(1, 2, 3, 1);
            Tuple expected = new(18, 24, 33, 1);

            Assert.AreEqual(m * t, expected);
        }

        [TestMethod]
        public void TestIdentity()
        {
            Matrix m = new(new double[,]
            {
                {0, 1, 2, 4},
                {1, 2, 4, 8},
                {2, 4, 8, 16},
                {4, 8, 16, 32}
            });
            Assert.AreEqual(m * Matrix.Identity(4), m);

            Tuple t = new(1, 2, 3, 1);
            Assert.AreEqual(Matrix.Identity(4) * t, t);
        }

        #endregion

        #region Complex operations

        [TestMethod]
        public void TestTransposed()
        {
            Matrix m = new(new double[,]
            {
                {0, 9, 3, 0},
                {9, 8, 0, 8},
                {1, 8, 5, 3},
                {0, 0, 5, 8}
            });

            Matrix transposed = new(new double[,]
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
            Matrix m3x3 = new(new double[,]
            {
                {1, 5, 0},
                {-3, 2, 7},
                {0, 6, -3}
            });

            Matrix sub3x3 = new(new double[,]
            {
                {-3, 2},
                {0, 6}
            });

            Assert.AreEqual(m3x3.Submatrix(0, 2), sub3x3);


            // Submatrix of 4x4
            Matrix m4x4 = new(new double[,]
            {
                {-6, 1, 1, 6},
                {-8, 5, 8, 6},
                {-1, 0, 8, 2},
                {-7, 1, -1, 1}
            });

            Matrix sub4x4 = new(new double[,]
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
            Matrix m = new(new double[,]
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
            Matrix m = new(new double[,]
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
            Matrix m3x3 = new(new double[,]
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
            Matrix m4x4 = new(new double[,]
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
            Matrix m1 = new(new double[,]
            {
                {-5, 2, 6, -8},
                {1, -5, 1, 8},
                {7, 7, -6, -7},
                {1, -3, 7, 4}
            });

            Matrix inverse1 = new(new double[,]
            {
                {0.21805, 0.45113, 0.24060, -0.04511},
                {-0.80827, -1.45677, -0.44361, 0.52068},
                {-0.07895, -0.22368, -0.05263, 0.19737},
                {-0.52256, -0.81391,  -0.30075, 0.30639}
            });

            Assert.AreEqual(m1.Determinant(), 532);
            Assert.AreEqual(m1.Cofactor(2, 3), -160);
            Assert.IsTrue(Calc.Equals(inverse1.matrix[3, 2], (double)-160 / 532));
            Assert.IsTrue(Calc.Equals(inverse1.matrix[2, 3], (double)105 / 532));
            Assert.AreEqual(m1.Inverse(), inverse1);


            // Second
            Matrix m2 = new(new double[,]
            {
                {8, -5, 9, 2},
                {7, 5, 6, 1},
                {-6, 0, 9, 6},
                {-3, 0, -9, -4}
            });

            Matrix inverse2 = new(new double[,]
            {
                {-0.15385, -0.15385, -0.28205, -0.53846},
                {-0.07692, 0.12308, 0.02564, 0.03077},
                {0.35897, 0.35897, 0.43590, 0.92308},
                {-0.69231, -0.69231, -0.76923, -1.92308}
            });

            Assert.AreEqual(m2.Inverse(), inverse2);


            // Third
            Matrix m3 = new(new double[,]
            {
                {9, 3, 0, 9},
                {-5, -2, -6, -3},
                {-4, 9, 6, 4},
                {-7, 6, 6, 2}
            });

            Matrix inverse3 = new(new double[,]
            {
                {-0.04074, -0.07778, 0.14444, -0.22222},
                {-0.07778, 0.03333, 0.36667, -0.33333},
                {-0.02901, -0.14630, -0.10926, 0.12963},
                {0.17778, 0.06667, -0.26667, 0.33333}
            });

            Assert.AreEqual(m3.Inverse(), inverse3);
        }

        [TestMethod]
        public void TestMultiplyByInverse()
        {
            Matrix a = new(new double[,]
            {
                {3, -9, 7, 3},
                {3, -8, 2, -9},
                {-4, 4, 4, 1},
                {-6, 5, -1, 1}
            });

            Matrix b = new(new double[,]
            {
                {8, 2, 2, 2},
                {3, -1, 7, 0},
                {7, 0, 5, 4},
                {6, -2, 0, 5}
            });

            Matrix c = a * b;

            Assert.AreEqual(c * b.Inverse(), a);
        }

        #endregion

        #region Transformations

        [TestMethod]
        public void TestTranslation()
        {
            // Multiplying point by translation
            Point p = new(-3, 4, 5);
            Point expected = new(2, 1, 7);

            Assert.AreEqual(p.Translate(5, -3, 2), expected);


            // Multiplying point by an inverse of translation
            Point expectedInversed = new(-8, 7, 3);

            Assert.AreEqual(p.Translate(5, -3, 2, inverse: true), expectedInversed);


            // Multiplying vector shouldn't affect it
            Vector v = new(1, 2, 3);

            Assert.AreEqual(v.Translate(5, -3, 2), v);
        }

        [TestMethod]
        public void TestScaling()
        {
            // Multiplying point by scaling
            Point p = new(-4, 6, 8);
            Point pExpected = new(-8, 18, 32);

            Assert.AreEqual(p.Scale(2, 3, 4), pExpected);


            // Multiplying vector by scaling
            Vector v = new(-4, 6, 8);
            Vector vExpected = new(-8, 18, 32);

            Assert.AreEqual(v.Scale(2, 3, 4), vExpected);


            // Multiplying by an inverse of scaling
            Vector vExpectedInversed = new(-2, 2, 2);

            Assert.AreEqual(v.Scale(2, 3, 4, inverse: true), vExpectedInversed);


            // Reflecting (Scaling by opposite)
            Point refExpected = new(4, 6, 8);

            Assert.AreEqual(p.Scale(-1, 1, 1), refExpected);
        }

        [TestMethod]
        public void TestRotationX()
        {
            // Half quarter rotation
            Point p = new(0, 1, 0);
            Point expectedHalfQuarter = new(0, 0.707106, 0.707106);

            Assert.AreEqual(p.RotateX(PI / 4), expectedHalfQuarter);


            // Full quarter rotation
            Point expectedFullQuarter = new(0, 0, 1);

            Assert.AreEqual(p.RotateX(PI / 2), expectedFullQuarter);


            // By an inverse
            Point expectedInversedHalfQuarter = new(0, 0.707106, -0.707106);

            Assert.AreEqual(p.RotateX(PI / 4, inverse: true), expectedInversedHalfQuarter);
        }

        [TestMethod]
        public void TestRotationY()
        {
            // Half quarter rotation
            Point p = new(0, 0, 1);
            Point expectedHalfQuarter = new(0.707106, 0, 0.707106);

            Assert.AreEqual(p.RotateY(PI / 4), expectedHalfQuarter);


            // Full quarter rotation
            Point expectedFullQuarter = new(1, 0, 0);

            Assert.AreEqual(p.RotateY(PI / 2), expectedFullQuarter);
        }

        [TestMethod]
        public void TestRotationZ()
        {
            // Half quarter rotation
            Point p = new(0, 1, 0);
            Point expectedHalfQuarter = new(-0.707106, 0.707106, 0);

            Assert.AreEqual(p.RotateZ(PI / 4), expectedHalfQuarter);


            // Full quarter rotation
            Point expectedFullQuarter = new(-1, 0, 0);

            Assert.AreEqual(p.RotateZ(PI / 2), expectedFullQuarter);
        }

        [TestMethod]
        public void TestSkewing()
        {
            Point p = new(2, 3, 4);

            Assert.AreEqual(p.Skew(1, 0, 0, 0, 0, 0), new Point(5, 3, 4));
            Assert.AreEqual(p.Skew(0, 1, 0, 0, 0, 0), new Point(6, 3, 4));
            Assert.AreEqual(p.Skew(0, 0, 1, 0, 0, 0), new Point(2, 5, 4));
            Assert.AreEqual(p.Skew(0, 0, 0, 1, 0, 0), new Point(2, 7, 4));
            Assert.AreEqual(p.Skew(0, 0, 0, 0, 1, 0), new Point(2, 3, 6));
            Assert.AreEqual(p.Skew(0, 0, 0, 0, 0, 1), new Point(2, 3, 7));
        }

        [TestMethod]
        public void TestViewTransformation()
        {
            // Setup
            Point from, to;
            Vector up;


            // Default orientation
            from = new(0, 0, 0);
            to = new(0, 0, -1);
            up = new(0, 1, 0);

            Assert.AreEqual(Matrix.ViewTransformation(from, to, up), Matrix.Identity());


            // Looking pozitive in Z direction
            to = new(0, 0, 1);

            Assert.AreEqual(Matrix.ViewTransformation(from, to, up), Matrix.Scaling(-1, 1, -1));


            // Moves the world, not self
            from = new(0, 0, 8);
            to = new(0, 0, 0);

            Assert.AreEqual(Matrix.ViewTransformation(from, to, up), Matrix.Translation(0, 0, -8));


            // Arbitrary direction
            from = new(1, 3, 2);
            to = new(4, -2, 8);
            up = new(1, 1, 0);

            Matrix expected = new(new double[,]
            {
                { -0.50709, 0.50709, 0.67612, -2.36643 },
                { 0.76772, 0.60609, 0.12122, -2.82843 },
                { -0.35857, 0.59761, -0.71714, 0 },
                { 0, 0, 0, 1 }
            });

            Assert.AreEqual(Matrix.ViewTransformation(from, to, up), expected);
        }

        #endregion
    }
}
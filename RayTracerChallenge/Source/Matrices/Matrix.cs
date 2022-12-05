using static System.MathF;
using RT.Source.Vectors;

namespace RT.Source.Matrices
{
    public class Matrix
    {
        private const int STDSize = 4;

        public readonly float[,] matrix;
        private readonly int width;
        private readonly int height;

        #region Constructors

        // Standard size is 4x4
        public Matrix()
        {
            height = STDSize;
            width = STDSize;
            matrix = new float[width, height];
        }

        // Init with zeros
        public Matrix(int height, int width)
        {
            this.height = height;
            this.width = width;
            matrix = new float[height, width];
        }

        public Matrix(float[,] matrix)
        {
            this.matrix = matrix;
            height = matrix.GetLength(0);
            width = matrix.GetLength(1);
        }

        public static Matrix Identity(int size)
        {
            Matrix m = new(size, size);
            for (int i = 0; i < size; i++)
                m.matrix[i, i] = 1;

            return m;
        }

        public static Matrix Identity() => Identity(STDSize);

        #endregion

        #region Operators

        public static bool operator ==(Matrix a, Matrix b)
        {
            if (a.width != b.width || a.height != b.height)
                return false;

            for (int row = 0; row < a.height; row++)
                for (int col = 0; col < a.width; col++)
                    if (!Calc.Equals(a.matrix[row, col], b.matrix[row, col]))
                        return false;

            return true;
        }

        public static bool operator !=(Matrix a, Matrix b) => !(a == b);

        public override bool Equals(object? obj)
        {
            if (obj is not Matrix item)
                return false;

            return this == item;
        }

        public override int GetHashCode() => HashCode.Combine(matrix);

        // Error prevention
        private static void AssertMatricesAreMultipliable(Matrix a, Matrix b)
        {
            if (a.width != b.height)
                throw new ArgumentException($"Matrices are not multipliable: width {a.width} != height {b.height}");
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            AssertMatricesAreMultipliable(a, b);
            Matrix m = new(a.height, b.width);
            for (int row = 0; row < a.height; row++)
                for (int col = 0; col < b.width; col++)
                    for (int i = 0; i < b.height; i++)
                        m.matrix[row, col] += a.matrix[row, i] * b.matrix[i, col];

            return m;
        }

        public static Vectors.Tuple operator *(Matrix m, Vectors.Tuple t)
        {
            Matrix tuple_matrix = new(new float[,] { { t.x }, { t.y }, { t.z }, { t.w } });
            Matrix mult = m * tuple_matrix;

            return new Vectors.Tuple(mult.matrix[0, 0], mult.matrix[1, 0], mult.matrix[2, 0], mult.matrix[3, 0]);
        }

        public override string ToString()
        {
            string res = "{ ";
            for (int row = 0; row < height; row++)
                // Get single row as a string
                res += $"{{{string.Join(", ", Enumerable.Range(0, width).Select(x => matrix[row, x].ToString()).ToArray())}}}, ";
            res += "}";
            return res;
        }

        #endregion

        #region Complex operations

        public Matrix Transposed()
        {
            Matrix m = new(width, height);
            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                    m.matrix[col, row] = matrix[row, col];

            return m;
        }

        internal Matrix Submatrix(int removeRow, int removeCol)
        {
            int skipped_row = 0;
            int skipped_col;

            Matrix m = new(width - 1, height - 1);
            for (int row = 0; row < height; row++)
            {
                if (row == removeRow)
                {
                    skipped_row = 1;
                    continue;
                }

                skipped_col = 0;
                for (int col = 0; col < width; col++)
                {
                    if (col == removeCol)
                    {
                        skipped_col = 1;
                        continue;
                    }
                    m.matrix[row - skipped_row, col - skipped_col] = matrix[row, col];
                }
            }

            return m;
        }

        internal float Minor(int removeRow, int removeCol) => Submatrix(removeRow, removeCol).Determinant();

        // Negate if removeRow + removeCol is odd
        internal float Cofactor(int removeRow, int removeCol) => Minor(removeRow, removeCol) * (((removeRow + removeCol) % 2 == 0) ? 1 : -1);

        private void AssertIsSquareMatrix()
        {
            if (width != height)
                throw new ArgumentException($"Matrix is not square: width {width} != height {height}");
        }

        internal float Determinant()
        {
            AssertIsSquareMatrix();

            // For 2x2 matrices
            if (width == 2)
                return (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);

            // Bigger than 2x2
            float det = 0;
            for (int col = 0; col < width; col++)
                det += matrix[0, col] * Cofactor(0, col);

            return det;
        }

        public Matrix Inverse()
        {
            float det = Determinant();

            if (det == 0)
                throw new ArgumentException("Matrix is not inversible: Determinant == 0");

            Matrix m = new(height, width);
            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                    // [col, row] for transposing
                    m.matrix[col, row] = Cofactor(row, col) / det;

            return m;
        }

        #endregion

        #region Transformations

        public static Matrix Translation(float x, float y, float z, bool inverse = false)
        {
            Matrix m = Identity();
            m.matrix[0, 3] = x;
            m.matrix[1, 3] = y;
            m.matrix[2, 3] = z;

            return inverse ? m.Inverse() : m;
        }

        public static Matrix Scaling(float x, float y, float z, bool inverse = false)
        {
            Matrix m = Identity();
            m.matrix[0, 0] = x;
            m.matrix[1, 1] = y;
            m.matrix[2, 2] = z;

            return inverse ? m.Inverse() : m;
        }

        public static Matrix RotationX(float rad, bool inverse = false)
        {
            Matrix m = Identity();
            m.matrix[1, 1] = Cos(rad);
            m.matrix[1, 2] = -Sin(rad);
            m.matrix[2, 1] = Sin(rad);
            m.matrix[2, 2] = Cos(rad);

            return inverse ? m.Inverse() : m;
        }

        public static Matrix RotationY(float rad, bool inverse = false)
        {
            Matrix m = Identity();
            m.matrix[0, 0] = Cos(rad);
            m.matrix[0, 2] = Sin(rad);
            m.matrix[2, 0] = -Sin(rad);
            m.matrix[2, 2] = Cos(rad);

            return inverse ? m.Inverse() : m;
        }

        public static Matrix RotationZ(float rad, bool inverse = false)
        {
            Matrix m = Identity();
            m.matrix[0, 0] = Cos(rad);
            m.matrix[0, 1] = -Sin(rad);
            m.matrix[1, 0] = Sin(rad);
            m.matrix[1, 1] = Cos(rad);

            return inverse ? m.Inverse() : m;
        }

        public static Matrix Skewing(float xy, float xz, float yx, float yz, float zx, float zy, bool inverse = false)
        {
            Matrix m = Identity();
            m.matrix[0, 1] = xy;
            m.matrix[0, 2] = xz;
            m.matrix[1, 0] = yx;
            m.matrix[1, 2] = yz;
            m.matrix[2, 0] = zx;
            m.matrix[2, 1] = zy;

            return inverse ? m.Inverse() : m;
        }

        public static Matrix ViewTransformation(Point from, Point to, Vector upDirection)
        {
            Vector forward = Vector.Normalize(new(to - from));
            Vector up = Vector.Normalize(upDirection);
            Vector left = Vector.CrossProduct(forward, up);
            Vector trueUp = Vector.CrossProduct(left, forward);

            Matrix orientation = new(new float[,]
            {
                { left.x, left.y, left.z, 0 },
                { trueUp.x, trueUp.y, trueUp.z, 0 },
                { -forward.x, -forward.y, -forward.z, 0 },
                { 0, 0, 0, 1 }
            });

            return orientation * Translation(-from.x, -from.y, -from.z);
        }

        // TODO: Switch this and that
        public Matrix Translate(float x, float y, float z, bool inverse = false) => Translation(x, y, z, inverse) * this;

        public Matrix Scale(float x, float y, float z, bool inverse = false) => Scaling(x, y, z, inverse) * this;

        public Matrix RotateX(float rad, bool inverse = false) => RotationX(rad, inverse) * this;

        public Matrix RotateY(float rad, bool inverse = false) => RotationY(rad, inverse) * this;

        public Matrix RotateZ(float rad, bool inverse = false) => RotationZ(rad, inverse) * this;

        public Matrix Skew(float xy, float xz, float yx, float yz, float zx, float zy, bool inverse = false)
            => Skewing(xy, xz, yx, yz, zx, zy, inverse) * this;

        #endregion
    }
}

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RayTracerTests")]
namespace RT.Source.Matrices
{
    public class Matrix
    {
        public readonly float[,] matrix;
        private readonly int width;
        private readonly int height;

        // Init with zeros
        public Matrix(int height, int width)
        {
            this.height = height;
            this.width = width;
            matrix = new float[height,width];
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

        public static bool operator !=(Matrix a, Matrix b) { return !(a == b); }

        public override bool Equals(object? obj)
        {
            if (obj is not Matrix item)
            {
                return false;
            }

            return this == item;
        }

        public override int GetHashCode() { return HashCode.Combine(matrix); }

        // Error prevention
        private static void AssertMatricesAreMultipliable(Matrix a, Matrix b)
        {
            if (a.width != b.height)
                throw new ArgumentException($"Matrices are not multipliable: height {a.width} != width {b.height}");
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
                if (row == removeRow) {
                    skipped_row = 1;
                    continue;
                }

                skipped_col = 0;
                for (int col = 0; col < width; col++)
                {
                    if (col == removeCol) {
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
        internal float Cofactor(int removeRow, int removeCol) => Minor(removeRow, removeCol) * (((removeRow + removeCol) % 2 == 0) ? 1: -1);

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
    }
}

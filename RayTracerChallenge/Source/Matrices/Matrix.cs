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

        #region Operators

        // For equality
        private static float[] FlattenMatrix(Matrix m) { return m.matrix.Cast<float>().ToArray(); }

        public static bool operator ==(Matrix a, Matrix b) 
        {
            return a.width == b.width && a.height == b.height && FlattenMatrix(a).SequenceEqual(FlattenMatrix(b));
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

        public static Matrix Identity(int size)
        {
            Matrix m = new(size, size);
            for (int i = 0; i < size; i++)
                m.matrix[i, i] = 1;

            return m;
        }

        #region Complex operations

        public Matrix Transposed()
        {
            Matrix m = new(width, height);
            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                    m.matrix[col, row] = matrix[row, col];

            return m;
        }

        #endregion
    }
}

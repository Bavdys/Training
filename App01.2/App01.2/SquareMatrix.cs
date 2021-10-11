using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App01._2
{
    /// <summary>
    ///  Represent class that stores an array of a square matrix.
    /// </summary>
    /// <typeparam name="T">The type of elements in the square matrix</typeparam>
    public class SquareMatrix<T>
    {
        protected const int SIZE_ARRAY_MATRIX = 1;
        protected T[] _matrix;

        protected virtual bool IsCorrectIndex(int i, int j)
        {
            return i < 0 || j < 0 || i > Size - 1 || j > Size - 1;
        }
       
        protected virtual void OnChangeValue(ValueEventArgs<T> e)
        {
            ChangeValue?.Invoke(this, e);
        }

        protected SquareMatrix() { }

        /// <summary>
        ///  Initializes a new instance of the class specified size of the array.
        /// </summary>
        /// <param name="size">Size of the array</param>
        public SquareMatrix(int size)
        {
            if (size < SIZE_ARRAY_MATRIX)
                throw new Exception($"The size of a square matrix must be greater than {SIZE_ARRAY_MATRIX}");

            Size = size;
            
            _matrix = new T[Size * Size];
        }
        
        /// <summary>
        ///  Initializes a new instance of the class specified an array.
        /// </summary>
        /// <param name="squareMatrix">Square matrix array</param>
        public SquareMatrix(T[] squareMatrix)
        {
            if (!(Math.Sqrt(squareMatrix.Length) % 1 == 0))
                throw new ArgumentException("The argument must be an array with the same size of rows and columns");

            Size = (int)Math.Sqrt(squareMatrix.Length);
            
            _matrix = new T[squareMatrix.Length];
            
            Array.Copy(squareMatrix, _matrix, squareMatrix.Length);
        }

        public int Size { get; protected set; }

        public virtual T this[int i, int j]
        {
            get
            {
                if(IsCorrectIndex(i, j))
                    throw new IndexOutOfRangeException("Index out of bounds of array");

                return _matrix[i * Size + j];
            }
            set
            {
                if (IsCorrectIndex(i, j))
                    throw new IndexOutOfRangeException("Index out of bounds of array");

                if (!_matrix[i * Size + j].Equals(value))
                    OnChangeValue(new ValueEventArgs<T>(i, j, _matrix[i * Size + j]));

                _matrix[i * Size + j] = value;
            }
        }

        public event EventHandler<ValueEventArgs<T>> ChangeValue;

        public override string ToString()
        {
            StringBuilder displayMatrix = new StringBuilder();
            for (int i = 0; i < Size ; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    displayMatrix.Append($"{this[i,j]}\t");
                }
                displayMatrix.Append('\n');
            }
            return displayMatrix.ToString();
        }
    }
}

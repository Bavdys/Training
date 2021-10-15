using System;
using System.Text;

namespace App01._2
{
    /// <summary>
    ///  Represent class that stores an array of a diagonal matrix.
    /// </summary>
    /// <typeparam name="T">The type of elements in the diagonal matrix</typeparam>
    public class DiagonalMatrix<T>: SquareMatrix<T>
    {
        /// <summary>
        ///  Initializes a new instance of the class specified size of the array.
        /// </summary>
        /// <param name="size">Size of the array</param>
        public DiagonalMatrix(int size)
        {
            if (size < SIZE_ARRAY_MATRIX)
                throw new Exception($"The size of a square matrix must be greater than {SIZE_ARRAY_MATRIX}");

            Size = size;

            _matrix = new T[Size];
        }
       
        /// <summary>
        ///  Initializes a new instance of the class specified an array.
        /// </summary>
        /// <param name="diagonalMatrix">Diagonal matrix array</param>
        public DiagonalMatrix(T[] diagonalMatrix):this(diagonalMatrix.Length)
        {
            Array.Copy(diagonalMatrix, _matrix, diagonalMatrix.Length);
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="i">Row</param>
        /// <param name="j">Column</param>
        /// <returns>Object of type T</returns>
        public override T this[int i, int j] 
        {
            get
            {
                if (IsCorrectIndex(i, j))
                {
                    throw new IndexOutOfRangeException("Index out of bounds of array");
                }

                return (i == j) ? _matrix[i] : default;
            }
            set
            {
                if (IsCorrectIndex(i, j))
                {
                    throw new IndexOutOfRangeException("Index out of bounds of array");
                }

                if (i != j)
                {
                    throw new Exception("Can only change elements on the diagonal");
                }

                if (!_matrix[i].Equals(value))
                {
                    OnChangeValue(new ValueEventArgs<T>(i, j, _matrix[i]));
                }

               _matrix[i] = value;
            }
        }
       
        /// <summary>
        ///  Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            StringBuilder displayMatrix = new StringBuilder();
            for (int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    if (i == j)
                        displayMatrix.Append($"{_matrix[i]}\t");
                    else
                        displayMatrix.Append("0\t");
                }
                displayMatrix.Append('\n');
            }
            return displayMatrix.ToString();
        }
    }
}

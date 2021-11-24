using System;

namespace App01._2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DiagonalMatrix<int> diagonalMatrix = new DiagonalMatrix<int>(new int[] { 1,3,5});
            diagonalMatrix.ChangeValue += ShowChange;

            diagonalMatrix[0, 0] = 21;

            Console.WriteLine(diagonalMatrix.ToString());

            SquareMatrix<int> squareMatrix = new SquareMatrix<int>(new int[] { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16});
            squareMatrix.ChangeValue += ShowChange;

            squareMatrix[3, 3] = 4;

            Console.WriteLine(squareMatrix.ToString());
        }
        static void ShowChange(object sender,ValueEventArgs<int> e)
        {
            Console.WriteLine($"Row:{e.Row} Column:{e.Column} Old number:{e.OldValue}");
        }
    }
}

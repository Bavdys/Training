using System;

namespace App01._2
{
    /// <summary>
    ///  Represents class that contain event data.
    /// </summary>
    /// <typeparam name="T">The type of elements</typeparam>
    public class ValueEventArgs<T>:EventArgs
    {
        /// <summary>
        ///  Initializes a new instance of the class specified row, column, old value.
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <param name="oldValue">Old value</param>
        public ValueEventArgs(int row, int column, T oldValue)
        {
            Row = row;
            Column = column;
            OldValue = oldValue;
        }

        /// <summary>
        /// Gets a row.
        /// </summary>
        public int Row { get;}
        /// <summary>
        /// Gets a column.
        /// </summary>
        public int Column { get;}
        /// <summary>
        /// Gets a old value.
        /// </summary>
        public T OldValue { get;}
    }
}

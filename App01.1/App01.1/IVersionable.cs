using System;

namespace App01._1
{
    /// <summary>
    /// Represent a version.
    /// </summary>
    interface IVersionable
    {
        /// <summary>
        /// Gets a byte array version.
        /// </summary>
        byte[] Version { get; }
    }
}

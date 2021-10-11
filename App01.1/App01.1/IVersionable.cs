using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using System;

namespace App01._1
{
    /// <summary>
    /// Represents a globally unique identifier.
    /// </summary>
    public interface IGuid
    {
        /// <summary>
        /// Gets or sets a globally unique identifier.
        /// </summary>
        Guid Guid { get; set; }
    }

    /// <summary>
    /// Represents the static class that generates a globally unique identifier. 
    /// </summary>
    public static class GenerationGuid
    {
        /// <summary>
        /// Is an extension method. Generates a globally unique identifier.
        /// </summary>
        /// <param name="guid">The GUID</param>
        public static void Generation(this IGuid guid)
        {
            guid.Guid = Guid.NewGuid();
        }
    }
}

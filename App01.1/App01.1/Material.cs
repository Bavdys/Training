using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App01._1
{
    /// <summary>
    /// Represent an abstract material class.
    /// </summary>
    abstract class Material: ICloneable, IGuid
    {
        const int LINE_LENGTH_DISCRIPTION = 256;
        string _discription;

        /// <summary>
        /// Initializes a default value.
        /// </summary>
        public Material()
        {
            this.Generation();
        }
        
        /// <summary>
        /// Gets or sets GUID.
        /// </summary>
        public Guid Guid { get; set; }
        
        /// <summary>
        /// Gets or sets text discription.
        /// </summary>
        public string Discription
        {
            get
            {
                return _discription;
            }
            set
            {
                if (value.Length > LINE_LENGTH_DISCRIPTION)
                    throw new ArgumentOutOfRangeException($"String length is more than {LINE_LENGTH_DISCRIPTION} characters");

                _discription = value;
            }
        }
      
        /// <summary>
        /// Override a method Equals().
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns>True if the value of a is the same as the value of b; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() == this.GetType() && this.Guid.Equals((obj as IGuid).Guid);
        }
        
        /// <summary>
        /// Returns a deep copy of the object. 
        /// </summary>
        /// <returns>A new object</returns>
        public abstract object Clone();
    }
}

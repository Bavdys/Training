using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App01._1
{
    /// <summary>
    /// Represent a lesson class.
    /// </summary>
    class Lesson: IVersionable, ICloneable, IGuid
    {
        const int LINE_LENGTH_DISCRIPTION = 256;
        const int SIZE_ARRAY_VERSION = 8;
        string _discription;
        byte[] _version;
        Material[] _material;

        /// <summary>
        /// Initializes a new instance with the specified a Material class array and a byte array.
        /// </summary>
        /// <param name="material">Material class array</param>
        /// <param name="version">A byte array</param>
        public Lesson(Material[] material, byte[] version)
        {
            Materials = material;
            Version = version;
            this.Generation();
        }
        
        /// <summary>
        /// Initializes a new instance with the specified a Material class array, a byte array, text discription.
        /// </summary>
        /// <param name="material">Material class array</param>
        /// <param name="version">A byte array</param>
        /// <param name="discription">Text discription</param>
        public Lesson(Material[] material, byte[] version, string discription) : this(material, version)
        {
            Discription = discription;
        }
        
        /// <summary>
        /// Gets or sets Material class array.
        /// </summary>
        public Material[] Materials 
        {
            get
            {
                return _material;
            }
            private set
            {
                _material = new Material[value.Length];

                for (int i = 0; i < value.Length; i++)
                    Materials[i] = (Material)value[i].Clone();
            }
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
        /// Gets a byte array.
        /// </summary>
        public byte[] Version 
        {
            get 
            { 
                return _version; 
            }
            private set
            {
                if (value.Length != SIZE_ARRAY_VERSION)
                    throw new ArgumentOutOfRangeException($"Array must contain {SIZE_ARRAY_VERSION} bytes");

                _version = new byte[SIZE_ARRAY_VERSION];
               
                Array.Copy(value, _version, value.Length);
            }
        }

        /// <summary>
        /// Represent lesson type.
        /// </summary>
        /// <returns>Lesson type</returns>
        public LessonType GetLessonType()
        {
            for(int i=0;i<Materials.Length;i++)
            {
                if (Materials[i] is VideoMaterial)
                    return LessonType.VideoLesson;
            }
            return LessonType.TextLesson;
        }
        
        /// <summary>
        /// Overrides method ToString().
        /// </summary>
        /// <returns>A new string</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            
            foreach (var item in Version)
                builder.Append($"{item}  ");
            
            return string.Format($"GUID: {Guid}\nVersion: {builder}\nDiscription: {Discription}\n\n\n");
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
        public object Clone()
        {
            return new Lesson(Materials, Version, Discription);
        }
    }

    /// <summary>
    /// Represent enum lesson type.
    /// </summary>
    enum LessonType
    {
        VideoLesson,
        TextLesson
    }
}

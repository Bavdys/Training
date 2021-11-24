using System;

namespace App01._1
{
    /// <summary>
    /// Represent text material.
    /// </summary>
    class TextMaterial : Material
    {
        const int LINE_LENGTH_TEXT = 10000;
        string _text;

        /// <summary>
        /// Initializes a new instance with the specified text material.
        /// </summary>
        /// <param name="text">Text material</param>
        public TextMaterial(string text) : base()
        {
            Text = text;
        }
        
        /// <summary>
        /// Initializes a new instance with the specified text material and text discription/
        /// </summary>
        /// <param name="text">Text material</param>
        /// <param name="discription"> Text discription</param>
        public TextMaterial(string text, string discription) : this(text)
        {
            Discription = discription;
        }
       
        /// <summary>
        /// Gets or sets text material.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public string Text {
            get
            {
                return _text;
            }
            private set
            {
                if (value.Length > LINE_LENGTH_TEXT)
                    throw new ArgumentOutOfRangeException($"String length is more than {LINE_LENGTH_TEXT} characters");
               
                _text = value;
            }
        }

        /// <summary>
        /// Overrides method ToString().
        /// </summary>
        /// <returns>A new string</returns>
        public override string ToString()
        {
            return string.Format($"TEXT\nGUID: {Guid}\nText: {Text}\nDiscription: {Discription}\n\n\n");
        }
        
        /// <summary>
        /// Returns a deep copy of the object. 
        /// </summary>
        /// <returns>A new object</returns>
        public override object Clone()
        {
            return new TextMaterial(Text, Discription);
        }
    }
}

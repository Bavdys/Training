using System;

namespace App01._1
{
    /// <summary>
    /// Represent network resource.
    /// </summary>
    class NetworkResource : Material
    {
        /// <summary>
        /// Initializes a new instance with the specified resource URI and type link.
        /// </summary>
        /// <param name="uri">Resource URI</param>
        /// <param name="typeLink">Type link</param>
        public NetworkResource(string uri, TypeLink typeLink) : base()
        {
            URIContent = uri;
            TypeLink = typeLink;
        }
        
        /// <summary>
        /// Initializes a new instance with the specified resource URI and type link and text discription.
        /// </summary>
        /// <param name="uri">Resource URI</param>
        /// <param name="typeLink">Type link</param>
        /// <param name="discription">Text discription</param>
        public NetworkResource(string uri, TypeLink typeLink, string discription) : this(uri, typeLink)
        {
            Discription = discription;
        }
        
        /// <summary>
        /// Gets a resource URI.
        /// </summary>
        public string URIContent { get; }
        
        /// <summary>
        /// Gets a type link.
        /// </summary>
        public TypeLink TypeLink { get; }

        /// <summary>
        /// Overrides method ToString().
        /// </summary>
        /// <returns>A new string</returns>
        public override string ToString()
        {
            return string.Format($"GUID: {Guid}\nURI Resource: {URIContent}\nTypeLink: {TypeLink.ToString()}\nDiscription: {Discription}\n\n\n");
        }
        
        /// <summary>
        /// Returns a deep copy of the object. 
        /// </summary>
        /// <returns>A new object</returns>
        public override object Clone()
        {
            return new NetworkResource(URIContent, TypeLink, Discription);
        }
    }
   
    /// <summary>
    /// Represent enum type link.
    /// </summary>
    enum TypeLink
    {
        Unknown,
        Html,
        Image,
        Audio,
        Video
    }
}

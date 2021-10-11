using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App01._1
{ 
    /// <summary>
    /// Represent video material.
    /// </summary>
    class VideoMaterial : Material, IVersionable
    {
        const int SIZE_ARRAY_VERSION = 8;
        byte[] _version;
       
        /// <summary>
        /// Initializes a new instance with the specified video URI, video format,a array byte, image URI. 
        /// </summary>
        /// <param name="uriContent">Video URI</param>
        /// <param name="videoFormat">Video format</param>
        /// <param name="version">Array byte</param>
        /// <param name="uriImage">Image URI</param>
        public VideoMaterial(string uriContent, VideoFormat videoFormat, byte[] version, string uriImage)
        {
            URIContent = uriContent;
            VideoFormat = videoFormat;
            Version = version;
            URIImage = uriImage;
        }
        
        /// <summary>
        /// Initializes a new instance with the specified video URI, video format,a array byte, image URI, text discription. 
        /// </summary>
        /// <param name="uriContent">Video URI</param>
        /// <param name="videoFormat">Video format</param>
        /// <param name="version">Array byte</param>
        /// <param name="uriImage">Image URI</param>
        /// <param name="discription">Text discription</param>
        public VideoMaterial(string uriContent, VideoFormat videoFormat, byte[] version, string uriImage, string discription) :
            this(uriContent, videoFormat, version, uriImage)
        {
            Discription = discription;
        }
      
        /// <summary>
        /// Gets a resource URI.
        /// </summary>
        public string URIContent { get;}
       
        /// <summary>
        /// Gets a image URI.
        /// </summary>
        public string URIImage { get; }
       
        /// <summary>
        /// Gets a video format.
        /// </summary>
        public VideoFormat VideoFormat { get;}
       
        /// <summary>
        /// Gets a byte array version.
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
        /// Overrides method ToString().
        /// </summary>
        /// <returns>A new string</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            
            foreach (var item in Version)
                builder.Append($"{item}  ");

            return string.Format($"GUID: {Guid}\nVersion: {builder.ToString()}\nURI Video: {URIContent}\nURI Image: {URIImage}\n" +
                $"Video format: {VideoFormat.ToString()}\nDiscription: {Discription}\n\n\n");
        }
       
        /// <summary>
        /// Returns a deep copy of the object. 
        /// </summary>
        /// <returns>A new object</returns>
        public override object Clone()
        {
            return new VideoMaterial(URIContent, VideoFormat, Version, URIImage, Discription);
        }
    }

    /// <summary>
    /// Represent enum video format.
    /// </summary>
    enum VideoFormat
    {
        Unknown,
        Avi,
        Mp4,
        Flv
    }

}

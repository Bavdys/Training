using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace App02._2
{
    [Serializable]
    public class Window
    {
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName ="top")]
        public int? Top { get; set; }
        
        [XmlElement(ElementName = "left")]
        public int? Left { get; set; }
       
        [XmlElement(ElementName = "width")]
        [DefaultValue(40)]
        public int? Width { get; set; }
        
        [XmlElement(ElementName = "height")]
        public int? Height { get; set; }

        public Window() { }

        public override string ToString()
        {
            return string.Format($"{Title}({(Top.HasValue ? Top.ToString() : " ? ")}, " +
                        $"{(Left.HasValue ? Left.ToString() : "?")}, {(Width.HasValue ? Width.ToString() : "?")}, " +
                        $"{(Height.HasValue ? Height.ToString() : "?")})");
        }
    }
}

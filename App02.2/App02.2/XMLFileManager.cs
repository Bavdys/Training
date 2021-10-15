using System;
using System.Xml.Linq;

namespace App02._2
{
     class XMLFileManager
    {
        public  XDocument ReadXMLFile(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("The string should not be empty");
            }    

            XDocument xmlDocument = XDocument.Load(path);
            
            return xmlDocument;
        }
    }
}

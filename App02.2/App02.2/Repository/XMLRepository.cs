using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace App02._2
{
    public class XMLRepository:IReadRepository
    {
        public List<Login> Read(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("The string should not be empty");
            }    

            XDocument xmlDocument = XDocument.Load(path);

            List<Login> Logins = new List<Login>
                (from itemLogin in xmlDocument.Element("config").Elements("login")
                select new Login
                {
                    Name = itemLogin.Attribute("name").Value,
                    Windows = new List<Window>
                    (from itemWindow in itemLogin.Elements("window")
                     select new Window
                     {
                         Title = itemWindow.Attribute("title").Value,
                         Top = itemWindow.Element("top")?.Value != null ? Convert.ToInt32(itemWindow.Element("top").Value) : (int?)null,
                         Left = itemWindow.Element("left")?.Value != null ? Convert.ToInt32(itemWindow.Element("left").Value) : (int?)null,
                         Width = itemWindow.Element("width")?.Value != null ? Convert.ToInt32(itemWindow.Element("width").Value) : (int?)null,
                         Height = itemWindow.Element("height")?.Value != null ? Convert.ToInt32(itemWindow.Element("height").Value) : (int?)null
                     })
                });
            
            return Logins;
        }
    }
}

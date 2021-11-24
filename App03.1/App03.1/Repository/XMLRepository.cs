using App03._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App03._1
{
    public class XMLRepository : IRepository
    {
        public SensorsCollectionModel LoadFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("The string should not be empty");
            }

            XDocument xmlDocument = XDocument.Load(path);

            SensorsCollectionModel sensorsCollectionModel = new SensorsCollectionModel();

            sensorsCollectionModel.Sensors = new List<SensorModel>
                (from itemSensor in xmlDocument.Element("sensors").Elements("sensor")
                 select new SensorModel
                 {
                     Various = itemSensor.Element("various").Value,
                     Min = Convert.ToInt32(itemSensor.Element("min").Value),
                     Max = Convert.ToInt32(itemSensor.Element("max").Value),
                     MeasuredValue = Convert.ToInt32(itemSensor.Element("measuredValue").Value)
                 });
            
            return sensorsCollectionModel;
        }
    }
}

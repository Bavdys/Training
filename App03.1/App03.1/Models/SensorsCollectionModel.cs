using System.Collections.Generic;

namespace App03._1.Models
{
    public class SensorsCollectionModel
    {
        public SensorsCollectionModel()
        {

        }

        public List<SensorModel> Sensors { get; set; } = new List<SensorModel>();
    }
}

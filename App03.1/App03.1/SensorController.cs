using App03._1.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace App03._1
{
    public class SensorController
    {
        public ObservableCollection<Sensor> Sensors { get; set; } = new ObservableCollection<Sensor>();

        public void LoadFromFile(string path, IRepository repository)
        {
            SensorsCollectionModel sensorsCollectionModel = repository.LoadFromFile(path);

            foreach (var itemSensor in sensorsCollectionModel.Sensors)
            {
                VariousSensor MyStatus = (VariousSensor)Enum.Parse(typeof(VariousSensor), itemSensor.Various, true);
                MeasurementInterval measurementInterval = new MeasurementInterval(itemSensor.Min, itemSensor.Max);

                Sensors.Add(new Sensor(MyStatus, measurementInterval, itemSensor.MeasuredValue));
            }
        }
        public void RemoveSensor(Sensor sensor)
        {
            Sensors.Remove(sensor);
        }
        public void AddSensor(Sensor sensor)
        {
            Sensors.Add(sensor);
        }
        public void SequentialSwitchingModes(Sensor sensor)
        {
            Sensors.FirstOrDefault(tempSensor => tempSensor.Equals(sensor)).SequentialSwitchingModes();
        }
        public void SwitchingModes(Sensor sensor)
        {
            Sensors.FirstOrDefault(tempSensor => tempSensor.Equals(sensor)).SwitchingModes();
        }
    }
}

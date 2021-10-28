using App03._1.Models;
using System;
using System.Linq;

namespace App03._1
{
    public class SensorController
    {
        public CollectionSensors CollectionSensors { get; private set; } = new CollectionSensors();

        public void LoadFromFile(string path, IRepository repository)
        {
            SensorsCollectionModel sensorsCollectionModel = repository.LoadFromFile(path);

            foreach (var itemSensor in sensorsCollectionModel.Sensors)
            {
                VariousSensor MyStatus = (VariousSensor)Enum.Parse(typeof(VariousSensor), itemSensor.Various, true);
                MeasurementInterval measurementInterval = new MeasurementInterval(itemSensor.Min, itemSensor.Max);

                CollectionSensors.Sensors.Add(new Sensor(MyStatus, measurementInterval, itemSensor.MeasuredValue));
            }
        }
        public void RemoveSensor(Sensor sensor)
        {
            CollectionSensors.Sensors.Remove(sensor);
        }
        public void AddSensor(Sensor sensor)
        {
            CollectionSensors.Sensors.Add(sensor);
        }
        public void SequentialSwitchingModes(Sensor sensor)
        {
            CollectionSensors.Sensors.FirstOrDefault(tempSensor => tempSensor.Equals(sensor)).SequentialSwitchingModes();
        }
        public void SwitchingModes(Sensor sensor)
        {
            CollectionSensors.Sensors.FirstOrDefault(tempSensor => tempSensor.Equals(sensor)).SwitchingModes();
        }
    }
}

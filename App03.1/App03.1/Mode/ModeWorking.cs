using System;
using System.Threading;

namespace App03._1
{
    public class ModeWorking:AbstractMode
    {
        private static ModeWorking instance;
        private static readonly object Locker = new object();

        private ModeWorking()
        {

        }

        public static ModeWorking GetInstance()
        {
            lock (Locker)
            {
                return instance ?? (instance = new ModeWorking());
            }
        }
        public override void Switch(Mode mode,Sensor sensor)
        {
            Random random = new Random();

            sensor.VariousState = VariousState.Working;

            for (int j = 0; j < 5; j++)
            {
                sensor.CurrentValue = random.Next(sensor.MeasurementInterval.Min, sensor.MeasurementInterval.Max);
                    
                Thread.Sleep(1000);
            }

            mode.State = ModeSimple.GetInstance();
        }
    }
}

using System.Threading;

namespace App03._1
{
    public class ModeCalibration:AbstractMode
    {
        private static ModeCalibration instance;
        private static readonly object Locker = new object();

        private ModeCalibration()
        {

        }

        public static ModeCalibration GetInstance()
        {
            lock (Locker)
            {
                return instance ?? (instance = new ModeCalibration());
            }
        }
        public override void Switch(Mode mode,Sensor sensor)
        {
            sensor.VariousState = VariousState.Calibration;
            
            while (sensor.CurrentValue < sensor.MeasurementInterval.Max)
            {
                sensor.CurrentValue += sensor.MeasuredValue;
                    
                Thread.Sleep(1000);
            }

            mode.State = ModeWorking.GetInstance();
        }
    }
}

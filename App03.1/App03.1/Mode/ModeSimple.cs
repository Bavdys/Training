using System.Threading;

namespace App03._1
{
    public class ModeSimple : AbstractMode
    {
        private static  ModeSimple instance;
        private static readonly object Locker = new object();

        private ModeSimple()
        {

        }

        public static ModeSimple GetInstance()
        {
            lock (Locker)
            {
                return instance ?? (instance = new ModeSimple());
            }
        }
        public override void Switch(Mode mode,Sensor sensor)
        {
            sensor.VariousState = VariousState.Simple;

            sensor.CurrentValue = sensor.MeasurementInterval.Min;
               
            Thread.Sleep(1000);

            mode.State = ModeCalibration.GetInstance();
        }
    }
}

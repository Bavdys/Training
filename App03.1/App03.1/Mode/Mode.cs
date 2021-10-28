namespace App03._1
{
    public class Mode
    {
        public AbstractMode State = ModeSimple.GetInstance();

        public void Switch(Sensor sensor)
        {
            State.Switch(this,sensor);
        }
    }
}

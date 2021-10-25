namespace Listener.Threshold
{
    public class Level:ILevel
    {   
        public Level(LevelValue levelValue)
        {
            Threshold = levelValue;
        }
        
        public LevelValue Threshold { get; set; }
    }
}

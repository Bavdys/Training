namespace Listener.LoggerModels
{
    public class ListenerModel
    {
        public ListenerModel()
        {

        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Assembly { get; set; }
        public string Source { get; set; }
        public LayoutModel Layout { get; set; } = new LayoutModel();
    }
}

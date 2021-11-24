namespace Listener
{
    public static class LoggerManager
    {
        public static Logger CreateLogger(ICreating creating)
        {
            return creating.CreateLogger();
        }
    }
}

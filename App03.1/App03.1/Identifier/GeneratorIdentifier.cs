using System;

namespace App03._1.Identifier
{
    public class GeneratorIdentifier
    {
        private static GeneratorIdentifier _instance;
        private static readonly object _locker = new object();
        private GeneratorIdentifier()
        {

        }
        public static GeneratorIdentifier GetInstance()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new GeneratorIdentifier());
            }
        }
        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }
    }
}

namespace App03._1
{
    public class XMLFactory : IFactory
    {
        public IRepository CreateRepository()
        {
            return new XMLRepository();
        }
    }
}

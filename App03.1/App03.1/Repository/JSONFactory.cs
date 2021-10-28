namespace App03._1
{
    public class JSONFactory : IFactory
    {
        public IRepository CreateRepository()
        {
            return new JSONRepository();
        }
    }
}

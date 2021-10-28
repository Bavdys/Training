using App03._1.Models;

namespace App03._1
{
    public interface IRepository
    {
        SensorsCollectionModel LoadFromFile(string path);
    }
}

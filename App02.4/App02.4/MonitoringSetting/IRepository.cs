using App2._4.MonitoringModel;

namespace App2._4.MonitoringSetting
{
    interface IRepository
    {
        string PathFile { get; }
        AuditsCollectionModel LoadFromFile();
    }
}

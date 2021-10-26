using App2._4.Message;

namespace App2._4
{
    interface IAuditor
    {
        int Interval { get; set; }
        int MaximumResponse { get; set; }
        string Url { get; set; }
        IMessage Mail { get; set; }

        void StartChecking();
        void StopChecking();
    }
}

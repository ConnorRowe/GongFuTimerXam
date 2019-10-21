namespace GongFuTimer
{
    public interface ILocalNotification
    {
        void CreateNotification(string contentText, long delayMillis = 0);
    }
}
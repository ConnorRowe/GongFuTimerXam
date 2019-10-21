namespace GongFuTimer
{
    public interface ILocalNotification
    {
        void CreateNotification(string contentText);
        void CancelNotification();
    }
}
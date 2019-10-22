namespace GongFuTimer
{
    public interface ILocalNotification
    {
        void CreateNotification(string contentText, int teaType, int infusionNum);
        void CancelNotification();
    }

    public interface IStatusBarColour
    {
        void ChangeColour(string colourHex);
    }
}
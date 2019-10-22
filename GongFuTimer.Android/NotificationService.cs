
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using GongFuTimer;
using GongFuTimer.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationService))]
public class NotificationService : ILocalNotification
{
    public void CreateNotification(string contentText, int teaType, int infusionNum)
    {
        //Get context using CurrentActivity plugin
        var context = (MainActivity)CrossCurrentActivity.Current.Activity;

        //Get appropriate suffix for the infusion number

        string infusion = infusionNum.ToString();
        bool forceTh = false;
        if (infusionNum > 9 && infusionNum < 21)
            forceTh = true;

        char lastChar = infusion[infusion.Length - 1];

        if(forceTh == true)
        {
            infusion += "th";
        }
        else if (lastChar == '1')
        {
            infusion += "st";
        }
        else if(lastChar == '2')
        {
            infusion += "nd";
        }
        else if(lastChar == '3')
        {
            infusion += "rd";
        }
        else
        {
            infusion += "th";
        }

        //Get tea type colour

        string teaName = "tea";
        if (contentText != string.Empty)
        {
            teaName = contentText.ToLower().Trim();
        }

        Android.Graphics.Color notiColour = Android.Graphics.Color.Black;
        bool isColorized = false;
        if (teaType > -1)
        {
            isColorized = true;
            notiColour = Android.Graphics.Color.ParseColor(MainActivity.teaColorsHex[teaType]);
        }

        // Create the PendingIntent
        var intent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
        intent.AddFlags(ActivityFlags.ClearTop);
        var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);

        //Build the notification:
        var builder = new NotificationCompat.Builder(context, MainActivity.CHANNEL_ID)
                      .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                      .SetContentIntent(pendingIntent) // Start up this activity when the user clicks the intent.
                      .SetContentTitle("Timer Complete") // Set the title
                      .SetSmallIcon(Resource.Drawable.ic_notification) // This is the icon to display
                      .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                      .SetColor(notiColour)
                      .SetColorized(isColorized)
                      .SetContentText($"Your {infusion} infusion of {teaName} is ready!"); // the message to display.        

        // Finally, publish the notification:
        var notificationManager = NotificationManagerCompat.From(context);
        notificationManager.Notify(MainActivity.NOTIFICATION_ID, builder.Build());

        builder.Dispose();
    }

    public void CancelNotification()
    {
        //Get context using CurrentActivity plugin
        var context = (MainActivity)CrossCurrentActivity.Current.Activity;
        var notificationManager = NotificationManagerCompat.From(context);
        //Cancel notification via its ID
        notificationManager.Cancel(MainActivity.NOTIFICATION_ID);
    }
}

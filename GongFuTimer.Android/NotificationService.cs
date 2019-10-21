using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using GongFuTimer;
using GongFuTimer.Droid;
using Java.Lang;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

[assembly: Dependency(typeof(NotificationService))]
    public class NotificationService : ILocalNotification
    {
        public void CreateNotification()
        {

            var context = (MainActivity)CrossCurrentActivity.Current.Activity;

            // Pass the current button press count value to the next activity:
            var valuesForActivity = new Bundle();
            valuesForActivity.PutInt(MainActivity.COUNT_KEY, context.Count);

            // Create the PendingIntent
            var intent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);


        //Build the notification:
        var builder = new NotificationCompat.Builder(context, MainActivity.CHANNEL_ID)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          .SetContentIntent(pendingIntent) // Start up this activity when the user clicks the intent.
                          .SetContentTitle("Timer Complete") // Set the title
                          .SetNumber(context.Count) // Display the count in the Content Info
                          .SetSmallIcon(Resource.Drawable.ic_stat_button_click) // This is the icon to display
                          .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                          //.SetContentText($"The button has been clicked {context.Count} times."); // the message to display.        
                          .SetContentText($"Your Gong Fu Timer has completed."); // the message to display.        

            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, builder.Build());

            // Increment the button press count:
            context.Count++;
        }
    }

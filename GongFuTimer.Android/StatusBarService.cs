using Android.OS;
using GongFuTimer;
using GongFuTimer.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarService))]
public class StatusBarService : IStatusBarColour
{
    public void ChangeColour(string colourHex)
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
            var c = (MainActivity)CrossCurrentActivity.Current.Activity;
            c?.RunOnUiThread(() => c.Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(colourHex)));
        }
    }
}

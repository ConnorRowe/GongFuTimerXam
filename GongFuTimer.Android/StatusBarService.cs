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
        int lollipop = (int)BuildVersionCodes.Lollipop;
        int sdkVer = (int)Build.VERSION.SdkInt;

        bool test = sdkVer >= lollipop;
        if (test)
        {
            var c = (MainActivity)CrossCurrentActivity.Current.Activity;
            c?.RunOnUiThread(() => c.Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(colourHex)));
        }
    }
}

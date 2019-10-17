using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GongFuTimer.ViewModel;

namespace GongFuTimer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PresetsPage : ContentPage
    {
        public PresetsPage()
        {
            InitializeComponent();
        }

        async void OnNewPresetClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewPresetPage
            {
                BindingContext = new Preset()
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            PresetsDatagrid.ItemsSource = await App.Database.GetPresetsAsync();
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            //var test = await App.Database.GetPresetsAsync();
            Preset preset = new Preset();
            preset.name = "Test";
            preset.altname = "AltTest";
            await App.Database.SavePresetAsync(preset);
        }
    }
}
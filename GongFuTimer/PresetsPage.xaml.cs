using GongFuTimer.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GongFuTimer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PresetsPage : ContentPage
    {
        TimerPage timerPage;

        public PresetsPage()
        {
            InitializeComponent();
        }
        public PresetsPage(TimerPage timerpage)
        {
            timerPage = timerpage;
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

        async private void ApplyButton_Clicked(object sender, EventArgs e)
        {
            timerPage.ApplyPreset(((Preset)BindingContext).SelectedPreset);
            await Navigation.PopAsync();
        }

        async private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Preset preset = ((Preset)BindingContext).SelectedPreset;
            ((Preset)BindingContext).SelectedPreset = null;
            await App.Database.DeletePresetAsync(preset);
            
            PresetsDatagrid.ItemsSource = await App.Database.GetPresetsAsync();

        }
    }
}
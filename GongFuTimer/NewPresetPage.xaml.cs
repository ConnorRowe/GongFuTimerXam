using GongFuTimer.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GongFuTimer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPresetPage : ContentPage
    {
        public NewPresetPage()
        {
            InitializeComponent();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            var preset = (Preset)BindingContext;
            await App.Database.SavePresetAsync(preset);
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
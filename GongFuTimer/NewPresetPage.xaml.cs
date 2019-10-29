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
        public NewPresetPage(bool isEditing, Preset preset)
        {
            InitializeComponent();

            if(isEditing)
            {
                nameEntry.Text = preset.name;
                baseEntry.Text = preset.baseseconds.ToString();
                plusEntry.Text = preset.plusseconds.ToString();
                tempEntry.Text = preset.temp.ToString();
                infEntry.Text = preset.maxinfusions.ToString();
                altnEntry.Text = preset.altname;

                int typeIndex = (int)preset.type;
                BindingContext = preset;
                typePicker.SelectedIndex = typeIndex;

                Title = "Edit a Preset";
            }
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
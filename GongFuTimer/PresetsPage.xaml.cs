﻿using GongFuTimer.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GongFuTimer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PresetsPage : ContentPage
    {
        TimerPage timerPage;
        bool isDelBtnConfirming = false;

        public PresetsPage(TimerPage timerpage)
        {
            timerPage = timerpage;
            InitializeComponent();
            presetListView.ItemTemplate = new DataTemplate(typeof(PresetListCell));
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

            (BindingContext as Preset).PresetCollection = new List<Preset>(await App.Database.GetPresetsAsync());
        }

        async private void ApplyButton_Clicked(object sender, EventArgs e)
        {
            timerPage.ApplyPreset(((Preset)BindingContext).SelectedPreset);
            await Navigation.PopAsync();
        }

        async private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var c = (Preset)BindingContext;

            if (!isDelBtnConfirming && c.SelectedPreset != null)
            {
                isDelBtnConfirming = true;
                ((Button)sender).Text = "Tap again to confirm";
            }
            else
            {
                Preset preset = c.SelectedPreset;
                if (preset != null)
                {

                    c.SelectedPreset = null;
                    await App.Database.DeletePresetAsync(preset);

                    c.PresetCollection = new List<Preset>(await App.Database.GetPresetsAsync());
                }

                isDelBtnConfirming = false;
                ((Button)sender).Text = "Delete";
            }
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            Preset editPreset = ((Preset)BindingContext).SelectedPreset;
            if (editPreset != null)
            {
                await Navigation.PushAsync(new NewPresetPage(true, editPreset));
            }
        }
    }
}
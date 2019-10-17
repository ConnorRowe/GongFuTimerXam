using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using GongFuTimer.Models;

namespace GongFuTimer.ViewModel
{
    class PresetsViewModel : INotifyPropertyChanged
    {
        public PresetsViewModel()
        {
            presetsList = Tea.getTestTeaList();
        }

        private List<Tea> presetsList;
        private Tea selectedTea = new Tea();

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public List<Tea> PresetsList
        {
            get { return presetsList; }
            set
            {
                if (presetsList != value)
                {
                    presetsList = value;
                    OnPropertyChanged();
                }
            }
        }

        public Tea SelectedTea
        {
            get { return selectedTea; }
            set
            {
                if (selectedTea != value)
                {
                    selectedTea = value;
                    OnPropertyChanged();
                }
            }
        }

        public void AddPreset(Tea newPreset)
        {
            presetsList.Add(newPreset);
            OnPropertyChanged("PresetsList");
        }

        public void RemovePreset(Tea Preset)
        {
            presetsList.Remove(Preset);
            OnPropertyChanged("PresetsList");
        }
    }
}

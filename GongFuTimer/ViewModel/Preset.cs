using SQLite;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace GongFuTimer.ViewModel
{
    public class Preset : INotifyPropertyChanged
    {
        public enum TeaType
        {
            Black, Green, Matcha, Oolong, RawPuehr, Ripened, White, Yellow, Tisane, MedicinalHerbs
        }

        public Preset()
        {
            teatypes = new List<string>()
            {
                "Black", "Green", "Matcha", "Oolong", "Raw Pu Ehr", "Ripened Pu Ehr", "White", "Yellow", "Tisane", "Medicinal Herbs"
            };
            isDataGridEnabled = true;
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public TeaType type { get; set; }
        public string altname { get; set; }
        public ushort baseseconds { get; set; }
        public ushort plusseconds { get; set; }
        public ushort temp { get; set; }
        public ushort maxinfusions { get; set; }

        //non-sql vars
        [Ignore]
        public List<string> teatypes { get; set; }
        [Ignore]
        private int selectedTypeIndex { get; set; }
        [Ignore]
        public int SelectedTypeIndex
        {
            get { return selectedTypeIndex; }
            set
            {
                if (selectedTypeIndex != value)
                {
                    selectedTypeIndex = value;
                    type = (TeaType)selectedTypeIndex;
                }
            }
        }

        [Ignore]
        private bool isDataGridEnabled { get; set; }
        [Ignore]
        public bool IsDataGridEnabled
        {
            get { return isDataGridEnabled; }
            set
            {
                if(isDataGridEnabled != value)
                {
                    isDataGridEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        [Ignore]
        private Preset selectedPreset { get; set; }
        [Ignore]
        public Preset SelectedPreset
        {
            get { return selectedPreset; }
            set
            {
                if(selectedPreset != value)
                {
                    selectedPreset = value;
                    OnPropertyChanged();

                    if (selectedPreset != null)
                    {

                        int rowColourIndex = (int)SelectedPreset.type;
                        if (rowColourIndex < 0)
                            rowColourIndex = 0;

                        RowColour = App.teaColoursDarkTxt[rowColourIndex];
                        OnPropertyChanged("RowColour");
                    }
                }
            }
        }

        [Ignore]
        public Color RowColour { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

using SQLite;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        private Preset selectedPreset { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        [Ignore]
        public Preset SelectedPreset
        {
            get { return selectedPreset; }
            set
            {
                if (selectedPreset != value)
                {
                    selectedPreset = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

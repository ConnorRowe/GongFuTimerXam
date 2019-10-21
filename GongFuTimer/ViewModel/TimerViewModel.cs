using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace GongFuTimer.ViewModel
{
    class TimerViewModel : INotifyPropertyChanged
    {

        public TimerViewModel(Action starttimercmd, Action resettimercmd)
        {
            StartTimerCommand = new Command(starttimercmd, () => !IsBusy);
            ResetTimerCommand = new Command(resettimercmd);
        }

        private string baseSecs = "0";
        private string plusSecs = "0";
        private string infNum = "0";
        private string timerOutput = "00:00:00";
        private bool isBusy = false;
        private string teaName = string.Empty;
        private string teaDetails = string.Empty;
        private string teaAltName = string.Empty;
        public int teaType { get; set; } = -1;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string BaseSecs
        {
            get { return baseSecs; }
            set
            {
                if (baseSecs != value)
                {
                    baseSecs = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PlusSecs
        {
            get { return plusSecs; }
            set
            {
                if (plusSecs != value)
                {
                    plusSecs = value;
                    OnPropertyChanged();
                }
            }
        }

        public string InfNum
        {
            get { return infNum; }
            set
            {
                if (infNum != value)
                {
                    infNum = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TimerOutput
        {
            get { return timerOutput; }
            set
            {
                if (timerOutput != value)
                {
                    timerOutput = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsBusy
        {
            get { return !isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TeaName
        {
            get { return teaName; }
            set
            {
                if (teaName != value)
                {
                    teaName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TeaDetails
        {
            get { return teaDetails; }
            set
            {
                if (teaDetails != value)
                {
                    teaDetails = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TeaAltName
        {
            get { return teaAltName; }
            set
            {
                if (teaAltName != value)
                {
                    teaAltName = value;
                    OnPropertyChanged();
                }
            }
        }

        public void formatTimerNum(Double remainingSecs)
        {
            double minutes = 0.0;
            double seconds = remainingSecs;
            double milliseconds = 0.0;

            if(seconds >= 60.0)
            {
                minutes = seconds / 60.0;
                minutes = Math.Floor(minutes);
                seconds -= minutes * 60.0;
            }
            milliseconds = (seconds - Math.Floor(seconds)) * 100.0f;
            seconds = Math.Floor(seconds);
            milliseconds = Math.Floor(milliseconds);

            string m = minutes.ToString();
            string s = seconds.ToString();
            string ms = milliseconds.ToString();

            if (minutes < 10.0)
                m = "0" + m;
            if (seconds < 10.0)
                s = "0" + s;
            if (milliseconds < 10.0)
                ms = "0" + ms;

            TimerOutput = string.Concat(m,":", s,":", ms);
        }

        public Command StartTimerCommand { get; }
        public Command ResetTimerCommand { get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.SimpleAudioPlayer;
using GongFuTimer.ViewModel;

namespace GongFuTimer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimerPage : ContentPage
    {
        DateTime LastFrameTime = DateTime.Now;
        double delta = double.MinValue;
        public Timer teaTimer = new Timer();
        ViewModel.TimerViewModel timerViewModel;
        private double targetSeconds = 0.0;
        double infNum = 0.0;
        ISimpleAudioPlayer alarmPlayer = CrossSimpleAudioPlayer.Current;

        public TimerPage()
        {
            InitializeComponent();
            teaTimer.Clear();
            timerViewModel = new ViewModel.TimerViewModel(StartTimer, ClearTimer);

            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"GongFuTimer.Audio." + "Alarm.wav");
            alarmPlayer.Load(stream);

            BindingContext = timerViewModel;

            SetToolbarColours(Color.OrangeRed, Color.White, true);
            // update loop
            Device.BeginInvokeOnMainThread(MainLoop);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void OnPresetClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PresetsPage(this)
            {
                BindingContext = new Preset()
            });            
        }

        public async void SetToolbarColours(Color bg, Color txt, bool shouldDelay)
        {
            if (shouldDelay)
                await Task.Delay(100);

            if (((NavigationPage)Application.Current.MainPage) != null)
            {
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = bg;
                ((NavigationPage)Application.Current.MainPage).BarTextColor = txt;
            }
        }

        public async void MainLoop()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    //calculate frame delta to prevent more than 30 frames from occuring every second
                    delta = (DateTime.Now - LastFrameTime).TotalMilliseconds;

                    if(delta >= 1000.0 / 30.0)
                    {
                        //Run update function
                        Update();

                        //Reset last frame time
                        LastFrameTime = DateTime.Now;
                    }

                    //Free up the processor for a bit
                    Task.Delay(16).Wait();
                }
            });
        }

        public void Update()
        {
            if(targetSeconds - teaTimer.ElapsedSeconds() <= 0.0 && teaTimer.isRunning)
            {
                teaTimer.Clear();
                timerViewModel.InfNum = infNum.ToString();
                timerViewModel.formatTimerNum(0.0);
                timerViewModel.IsBusy = false;
                alarmPlayer.Play();
            }

            if (teaTimer.isRunning)
                timerViewModel.formatTimerNum(targetSeconds - teaTimer.ElapsedSeconds());
        }

        public void StartTimer()
        {
            //target Seconds = base time + (additional infusion time * infusion number)
            targetSeconds = StoDo(timerViewModel.BaseSecs) + (StoDo(timerViewModel.PlusSecs) * infNum);
            ++infNum; timerViewModel.InfNum = infNum.ToString();

            if (targetSeconds > 0.0f)
            {
                teaTimer.Start();
                timerViewModel.IsBusy = true;
            }            
        }

        public void ClearTimer()
        {
            teaTimer.Clear();
            infNum = 0.0; timerViewModel.InfNum = infNum.ToString();
            timerViewModel.formatTimerNum(0.0);
            timerViewModel.IsBusy = false;
        }

        private double StoDo(string str)
        {
            double parsedString = double.MinValue;

            try
            {
                parsedString = double.Parse(str);
            }
            catch (Exception)
            {
            }

            return parsedString;
        }

        public void ApplyPreset(Preset preset)
        {
            TimerViewModel viewModel = (TimerViewModel)BindingContext;
            viewModel.BaseSecs = preset.baseseconds.ToString();
            viewModel.PlusSecs = preset.plusseconds.ToString();
            viewModel.TeaName = preset.name;
            viewModel.TeaAltName = preset.altname;
            viewModel.TeaDetails = String.Format("{0}, brew at {1}°C for {2} infusions.", App.teaTypeNames[(int)preset.type], preset.temp, preset.maxinfusions);
            SetToolbarColours(App.teaColoursDarkTxt[(int)preset.type], Color.Black, false);
        }
    }
}
﻿using GongFuTimer.ViewModel;
using Plugin.SimpleAudioPlayer;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GongFuTimer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimerPage : ContentPage
    {
        DateTime LastFrameTime = DateTime.Now;
        double delta = double.MinValue;
        public Timer teaTimer = new Timer();
        readonly TimerViewModel timerViewModel;
        private double targetSeconds = 0.0;
        double infNum = 0.0;
        ISimpleAudioPlayer alarmPlayer = CrossSimpleAudioPlayer.Current;
        private readonly ILocalNotification notificationService;
        private double tempInfNum = 0.0f;

        public TimerPage()
        {
            InitializeComponent();
            teaTimer.Clear();
            timerViewModel = new TimerViewModel(StartTimer, ClearTimer, PlusInfTemp, SubInfTemp, ResetInfTemp);
            notificationService = DependencyService.Get<ILocalNotification>();

            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"GongFuTimer.Audio." + "Alarm.wav");
            alarmPlayer.Load(stream);

            BindingContext = timerViewModel;

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

            NavigationPage navigationPage = (NavigationPage)Application.Current.MainPage;

            if (navigationPage != null)
            {
                navigationPage.BarBackgroundColor = bg;
                //navigationPage.BarTextColor = txt;
                var statusBarService = DependencyService.Get<IStatusBarColour>();
                statusBarService?.ChangeColour(DarkenColour(bg).ToHex());                    
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
                if(!App.IsInForeground)
                {
                    notificationService.CreateNotification(timerViewModel.TeaName, timerViewModel.teaType, (int)infNum);
                }
                else
                {
                    notificationService.CancelNotification();
                }

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
            if(infNum!= tempInfNum)
            {
                infNum = tempInfNum;
            }
            //target Seconds = base time + (additional infusion time * infusion number)
            targetSeconds = StoDo(timerViewModel.BaseSecs) + (StoDo(timerViewModel.PlusSecs) * infNum);
            ++infNum; timerViewModel.InfNum = infNum.ToString();
            tempInfNum = infNum;

            UpdateResetInfBtnVisibility();

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
            tempInfNum = infNum;
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
            if (preset != null)
            {
                TimerViewModel viewModel = (TimerViewModel)BindingContext;
                viewModel.BaseSecs = preset.baseseconds.ToString();
                viewModel.PlusSecs = preset.plusseconds.ToString();
                viewModel.TeaName = preset.name;
                viewModel.TeaAltName = preset.altname;
                viewModel.TeaDetails = String.Format("{0}, brew at {1}°C for {2} infusions.", App.teaTypeNames[(int)preset.type], preset.temp, preset.maxinfusions);
                viewModel.teaType = (int)preset.type;
                SetToolbarColours(App.teaColoursDarkTxt[(int)preset.type], Color.Black, false);
            }
        }

        private Color DarkenColour(Color colour)
        {
            double r = colour.R * 0.85;
            double g = colour.G * 0.85;
            double b = colour.B * 0.85;

            return new Color(r, g, b, colour.A);
        }

        public void PlusInfTemp()
        {
            ++tempInfNum; timerViewModel.InfNum = tempInfNum.ToString();
            UpdateResetInfBtnVisibility();
        }
        public void SubInfTemp()
        {
            if(tempInfNum > 0)
            {
                --tempInfNum; timerViewModel.InfNum = tempInfNum.ToString();
                UpdateResetInfBtnVisibility();
            }
        }
        public void ResetInfTemp()
        {
            tempInfNum = infNum; timerViewModel.InfNum = tempInfNum.ToString();
            UpdateResetInfBtnVisibility();
        }

        public bool IsResetChanged()
        {
            return infNum != tempInfNum;
        }

        private void UpdateResetInfBtnVisibility()
        {
            ((TimerViewModel)BindingContext).IsInfResetVisible = infNum != tempInfNum;
        }
    }
}
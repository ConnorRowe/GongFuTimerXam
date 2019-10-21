using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using GongFuTimer.Data;

namespace GongFuTimer
{
    public partial class App : Application
    {
        static PresetDatabase database;
        public static bool IsInForeground { get; set; } = false;

        //Colors for each tea type the same order as Preset.TeaType enum
        public static readonly Color[] teaColoursDarkTxt = new Color[10] { Color.FromHex("ac0000"), Color.FromHex("59914f"), Color.FromHex("057400"), Color.FromHex("535da1"), Color.FromHex("008487"), Color.FromHex("5a2213"), Color.FromHex("cde1db"), Color.FromHex("fed25a"), Color.FromHex("fcdcd8"), Color.FromHex("f36e21") };
        public static readonly string[] teaTypeNames = new string[10] { "Black", "Green", "Matcha", "Oolong", "Raw Pu Ehr", "Ripened Pu Ehr", "White", "Yellow", "Tisane", "Medicinal Herbs" };


        public static PresetDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new PresetDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Presets.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            Xamarin.Forms.DataGrid.DataGridComponent.Init();

            MainPage = new NavigationPage(new TimerPage());


            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;

        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("bc19ea2a-df07-4853-8237-ae718dc967e2", typeof(Push));
            IsInForeground = true;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            IsInForeground = false;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            IsInForeground = true;
        }


    }
}

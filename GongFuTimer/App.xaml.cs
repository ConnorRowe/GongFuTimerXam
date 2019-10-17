using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GongFuTimer.Data;

namespace GongFuTimer
{
    public partial class App : Application
    {
        static PresetDatabase database;

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
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AhvazpmuPortable
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AhvazpmuPortable.MainPage();
        }

        protected override void OnStart()
        {
            MobileCenter.LogLevel = LogLevel.Verbose;
            MobileCenter.Start("android=d2f12e62-a8a5-40e0-87c0-0ec92e7fd449;" +
                   "uwp={Your UWP App secret here};" +
                   "ios={Your iOS App secret here}",
                   typeof(Analytics), typeof(Crashes));
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

using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App.Services;
using App.Views;

namespace App
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string BackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.0.107:8000" : "http://localhost:5000";

        public App()
        {
            InitializeComponent();

            DependencyService.Register<SolicitacaoDataStore>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

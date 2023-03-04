using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using XamarinForm.Services;
//using XamarinForm.Views;

namespace XamarinForm
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
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

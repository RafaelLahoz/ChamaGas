using AppChamaGas.DataAccess;
using AppChamaGas.View;
using MonkeyCache.SQLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppChamaGas
{
    public partial class App : Application
    {

        public static string uriAzure = "https://chamagaslahoz.azurewebsites.net";
        public App()
        {
            InitializeComponent();
#if DEBUG
            HotReloader.Current.Run(this);
#endif
            Conexao.Initialize();
            //Habilita a pagina principal
            //MainPage = new MasterView();
            Barrel.ApplicationId = "ChamaGasCacheBd";
            //Remove dados expirados
            Barrel.Current.EmptyExpired();
            MainPage = new LoginView();
            
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
